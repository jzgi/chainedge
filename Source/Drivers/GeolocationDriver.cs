using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Runtime.CompilerServices;
using ChainFX;

namespace ChainEdge.Drivers
{
    public class GeolocationDriver : Driver
    {
        private SerialPort port = new SerialPort();


        protected internal override void OnCreate(object state)
        {
        }

        public override void Bind()
        {
            var names = SerialPort.GetPortNames().AddOf("COM1", "COM2");
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

        public override string Label => "定位";

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

        public void mySerialPort_DataRecieved(object sender, SerialDataReceivedEventArgs e)
        {
            //whatever logic and read procedure we want
        }
    }
}