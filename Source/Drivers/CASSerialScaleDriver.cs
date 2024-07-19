using System;
using System.IO.Ports;
using System.Text;
using System.Threading;

namespace ChainEdge.Drivers
{
    public class CASSerialScaleDriver : Driver
    {
        readonly SemaphoreSlim semaph = new(1);

        SerialPort port;


        protected internal override void OnCreate(object state)
        {
            port = new()
            {
                BaudRate = 9600,
                DataBits = 8,
                Parity = Parity.None,
                StopBits = StopBits.One,
                ReadTimeout = Period,
                WriteTimeout = Period,
            };
        }


        public override bool IsBound => port.IsOpen && bound;

        public override void Bind()
        {
            // try to clear current states
            try
            {
                bound = false;

                if (port.IsOpen)
                {
                    port.Close();
                }
            }
            catch (Exception e)
            {
            }

            // try each of the port names
            //
            var names = SerialPort.GetPortNames();
            foreach (var name in names)
            {
                semaph.Wait();
                try
                {
                    port.PortName = name;
                    port.Open();

                    // try a retrieval
                    if (TryGetInput(out _, Period))
                    {
                        bound = true;
                        return;
                    }

                    port.Close();
                }
                catch (UnauthorizedAccessException e) // occupied by other process
                {
                }
                catch (InvalidOperationException e) // port is open
                {
                }
                catch (Exception e)
                {
                    if (port.IsOpen)
                    {
                        port.Close();
                    }
                }
                finally
                {
                    semaph.Release();
                }
            }
        }


        public override string Label => "Ì¨³Ó";


        private static readonly byte[]
            ENQ = { 0x05 },
            DC1 = { 0x11 },
            buf = new byte[15];

        public override bool TryGetInput(out (decimal a, decimal b) result, int milliseconds)
        {
            result = default;

            semaph.Wait(milliseconds);
            try
            {
                Weigh:

                port.Write(ENQ, 0, ENQ.Length);

                Thread.Sleep(Period);

                Array.Clear(buf);
                var num = port.Read(buf, 0, 4);
                if (num <= 0 || buf[0] != 0x06)
                {
                    bound = false;
                    return false;
                }

                Array.Clear(buf);

                port.Write(DC1, 0, DC1.Length);

                // must pause for a period
                Thread.Sleep(Period);

                num = port.Read(buf, 0, buf.Length);
                if (num == 0)
                {
                    bound = false;
                    return false;
                }

                // SOH STX
                if (buf[0] != 0x01 || buf[1] != 0x02)
                {
                    bound = false;
                    return false;
                }
                var sta = (char)buf[2];
                if (sta == 'F' || sta == 'U') // overload or unstable
                {
                    if (sta == 'F') // unclear or overload 
                    {
                        port.Write("<ZK>\t");
                    }
                    goto Weigh;
                }

                var sb = new StringBuilder();

                sb.Append((char)buf[3]); // sign
                sb.Append((char)buf[4]);
                sb.Append((char)buf[5]);
                sb.Append((char)buf[6]);
                sb.Append((char)buf[7]);
                sb.Append((char)buf[8]);
                sb.Append((char)buf[9]);

                var v = decimal.Parse(sb.ToString());

                var un1 = (char)buf[10];
                var un0 = (char)buf[11];

                var bcc = buf[12];
                var etx = buf[13];
                var eot = buf[14];

                if (etx != 0x03 || eot != 0x04)
                {
                    bound = false;
                    return false;
                }

                if (un1 == 'k' && un0 == 'g')
                {
                    v *= 1000;
                }

                result.a = v;
                return true;
            }
            catch (Exception e)
            {
                bound = false;
                return false;
            }
            finally
            {
                semaph.Release();
            }
        }

        public void Tare()
        {
            port.Write("<TK>\t");
        }

        public void Zero()
        {
            port.Write("<ZK>\t");
        }

        public override void Stop()
        {
            base.Stop();

            port.Close();
        }
    }
}