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
            ReadTimeout = 500,
            WriteTimeout = 500,
        };


        public override void Reset()
        {
            foreach (var name in SerialPort.GetPortNames())
            {
                semaph.Wait();
                try
                {
                    port.PortName = name;
                    port.Open();

                    // make a retrieval
                    TryGetInput(out _, period);

                    if (status <= STU_VOID)
                    {
                        port.Close(); // continue
                    }
                    else
                    {
                        break; // keep COM port
                    }
                }
                catch (UnauthorizedAccessException e) // used by other process
                {
                }
                catch (InvalidOperationException e) // port is open
                {
                    port.Close();
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


        public override string Label => "台秤";


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
                var b = port.ReadByte();
                if (b != 0x06)
                {
                    status = STU_ERR;
                    return false;
                }

                port.Write(DC1, 0, DC1.Length);
                if (port.Read(buf, 0, buf.Length) == 0)
                {
                    status = STU_ERR;
                    return false;
                }

                // SOH STX
                if (buf[0] != 0x01 || buf[1] != 0x02)
                {
                    status = STU_ERR;
                    return false;
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
                    return false;
                }

                if (un1 == 'k' && un0 == 'g')
                {
                    v *= 1000;

                    result.a = v;
                }

                status = STU_READY;
                return true;
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