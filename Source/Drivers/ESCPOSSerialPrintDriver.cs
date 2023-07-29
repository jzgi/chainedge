using System;
using System.Diagnostics;
using System.IO.Ports;
using ChainEdge.Features;

namespace ChainEdge.Drivers
{
    public class ESCPOSSerialPrintDriver : Driver, IReceiptPrint
    {
        readonly SerialPort port = new()
        {
            BaudRate = 9600,
            DataBits = 8,
            Parity = Parity.None,
            StopBits = StopBits.One
        };


        public ESCPOSSerialPrintDriver()
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
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public string Func(string param)
        {
            return "Example: " + param;
        }


        private SerialPort serialport;


        void init()
        {
            serialport = new SerialPort("", 9600);

            serialport.DataReceived += mySerialPort_DataRecieved;
        }


        public void mySerialPort_DataRecieved(object sender, SerialDataReceivedEventArgs e)
        {
            //whatever logic and read procedure we want
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