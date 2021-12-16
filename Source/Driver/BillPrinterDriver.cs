using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SkyEdge.Driver
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ComVisible(true)]
    public class BillPrinterDriver : _Driver, INotePrint
    {
        private SerialPort port = new SerialPort();
        
        
        
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

        [IndexerName("Items")]
        public string this[int index]
        {
            get => m_dictionary[index];
            set => m_dictionary[index] = value;
        }

        private Dictionary<int, string> m_dictionary = new Dictionary<int, string>();


        private SerialPort serialport;


        void init()
        {
            serialport = new SerialPort("", 9600);

            serialport.DataReceived += mySerialPort_DataRecieved;
        }

        public void printBuyReceipt()
        {
            throw new System.NotImplementedException();
        }

        public void printShipList()
        {
            throw new System.NotImplementedException();
        }

        public void mySerialPort_DataRecieved(object sender, SerialDataReceivedEventArgs e)
        {
            //whatever logic and read procedure we want
        }
    }
}