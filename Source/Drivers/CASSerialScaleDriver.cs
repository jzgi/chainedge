using System;
using System.Diagnostics;
using System.IO.Ports;
using ChainEdge.Features;

namespace ChainEdge.Drivers
{
    public class CASSerialScaleDriver : Driver, IScale
    {
        readonly SerialPort port = new()
        {
            BaudRate = 9600,
            DataBits = 8,
            Parity = Parity.None,
            StopBits = StopBits.One
        };

        public CASSerialScaleDriver()
        {
            foreach (var name in SerialPort.GetPortNames())
            {
                port.PortName = name;
                try
                {
                    port.Open();

                    port.Close();
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            }
        }


        static byte[] ENQ = { 0x05 };

        public int Weigh()
        {
            port.Write(ENQ, 0, 1);

            port.ReadByte();

            return 0;
        }

        public void Tear()
        {
            port.Write("<TK>\t");
        }

        public void Zero()
        {
            port.Write("<ZK>\t");
        }

        public override void Test()
        {
            throw new NotImplementedException();
        }
    }
}