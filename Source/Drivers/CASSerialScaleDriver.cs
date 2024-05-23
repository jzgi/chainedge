using System;
using System.IO.Ports;
using System.Text;
using System.Threading;

namespace ChainEdge.Drivers
{
    public class CASSerialScaleDriver : Driver
    {
        readonly SemaphoreSlim semaph = new(1);

        readonly SerialPort port = new()
        {
            BaudRate = 9600,
            DataBits = 8,
            Parity = Parity.None,
            StopBits = StopBits.One,
            ReadTimeout = 200,
            WriteTimeout = 200,
        };


        public override void Rebind()
        {
            foreach (var name in SerialPort.GetPortNames())
            {
                semaph.Wait();
                try
                {
                    port.PortName = name;
                    port.Open();

                    // make a retrieval
                    if (TryObtain(out _, period))
                    {
                        return; // keep COM port
                    }

                    // continue
                    port.Close();
                }
                catch (UnauthorizedAccessException e) // used by other process
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

        public override bool TryObtain(out (decimal a, decimal b) result, int milliseconds)
        {
            result = default;

            semaph.Wait(milliseconds);
            try
            {
                Weigh:


                port.Write(ENQ, 0, ENQ.Length);

                Thread.Sleep(period);

                Array.Clear(buf);
                var num = port.Read(buf, 0, 4);
                if (num <= 0 || buf[0] != 0x06)
                {
                    status = STU_ERR;
                    return false;
                }

                // Thread.Sleep(period);

                Array.Clear(buf);

                port.Write(DC1, 0, DC1.Length);

                // must pause for a period
                Thread.Sleep(period);

                num = port.Read(buf, 0, buf.Length);
                if (num == 0)
                {
                    status = STU_ERR;
                    return false;
                }

                // SOH STX
                if (buf[0] != 0x01 || buf[1] != 0x02)
                {
                    status = STU_ERR;
                    return default;
                }
                var sta = (char)buf[2];
                if (sta == 'F' || sta == 'U') // overload or unstable
                {
                    if (sta == 'F') // unclear or overload 
                    {
                        port.Write("<ZK>\t");
                    }
                    status = STU_VOID;
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
                    status = STU_ERR;
                    return default;
                }

                if (un1 == 'k' && un0 == 'g')
                {
                    v *= 1000;
                }

                result.a = v;
                status = STU_READY;
                return true;
            }
            catch (Exception e)
            {
                status = STU_ERR;
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