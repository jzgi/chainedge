using System;
using System.Diagnostics;
using System.IO.Ports;

namespace ChainEdge.Drivers
{
    public class ESCPOSSerialPrintDriver : Driver
    {
        readonly SerialPort port = new()
        {
            BaudRate = 9600,
            DataBits = 8,
            Parity = Parity.None,
            StopBits = StopBits.One
        };


        public override void Test()
        {
            var names = SerialPort.GetPortNames();
            foreach (var name in names)
            {
                port.PortName = name;
                try
                {
                    port.Open();

                    port.Close();
                }
                catch (Exception e)
                {
                }
            }
        }

        public override string Label => "票据打印";

        public string Func(string param)
        {
            return "Example: " + param;
        }


        public void printBizlabel()
        {
            throw new NotImplementedException();
        }

        public void printBuyReceipt()
        {
            throw new NotImplementedException();
        }

        public void printShipList()
        {
            throw new NotImplementedException();
        }


        public void PrintTitle(string v)
        {
            throw new NotImplementedException();
        }

        public void PrintRow(short idx, string name, decimal price, short qty)
        {
            throw new NotImplementedException();
        }

        public void PrintBottomLn()
        {
            throw new NotImplementedException();
        }
    }
}