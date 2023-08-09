using System;
using System.Diagnostics;
using System.IO.Ports;
using System.Threading;
using ChainFx;

namespace ChainEdge.Drivers
{
    public class CASSerialScaleDriver : Driver
    {
        readonly SemaphoreSlim entrance = new(1);

        readonly SerialPort port = new()
        {
            BaudRate = 9600,
            DataBits = 8,
            Parity = Parity.None,
            StopBits = StopBits.One,
        };


        public override void Test()
        {
            foreach (var name in SerialPort.GetPortNames())
            {
                port.PortName = name;
                try
                {
                    port.Open();

                    if (TryGetInput(out (decimal v, JObj ext) result, 100))
                    {
                        return;
                    }

                    port.Close();
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            }
        }

        public override string Label => "台秤";

        public override bool TryGetInput(out (decimal v, JObj ext) result, int milliseconds)
        {
            result = default;

            entrance.Wait(milliseconds);

            var ret = TryWeigh(out var v);
            result = (v, null);

            entrance.Release();
            return ret;
        }

        static byte[] ENQ = { 0x57, 0x0D };
        static byte[] DC1 = { 0x11 };

        private static byte[] buf = new byte[16];

        public bool TryWeigh(out decimal v)
        {
            // port.Write(ENQ, 0, ENQ.Length);
            //
            // var b = port.ReadByte();
            //
            // if (b != 0x06)
            // {
            //     v = 0;
            //     return false;
            // }

            // port.Write(DC1, 0, 1);
            //
            // if (port.Read(buf, 0, buf.Length) > 0)
            // {
            //     v = 0;
            //     return false;
            // }
            //
            v = default;

            return true;
        }

        public void Tear()
        {
            port.Write("<TK>\t");
        }

        public void Zero()
        {
            port.Write("<ZK>\t");
        }
    }
}