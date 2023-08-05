using System;
using System.Diagnostics;
using System.IO.Ports;
using System.Threading;
using ChainEdge.Features;
using ChainFx;

namespace ChainEdge.Drivers
{
    public class CASSerialScaleDriver : Driver, IScale
    {
        readonly SemaphoreSlim entrance = new(1);

        readonly SerialPort port = new()
        {
            BaudRate = 9600,
            DataBits = 8,
            Parity = Parity.None,
            StopBits = StopBits.One
        };

        public CASSerialScaleDriver()
        {
        }

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


        public override bool TryGetInput(out (decimal v, JObj ext) result, int milliseconds)
        {
            result = default;

            entrance.Wait(milliseconds);

            var ret = TryWeigh(out var v);
            result = (v, null);

            entrance.Release();
            return ret;
        }

        static byte[] ENQ = { 0x05 };

        public bool TryWeigh(out decimal v)
        {
            port.Write(ENQ, 0, 1);

            port.ReadByte();

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