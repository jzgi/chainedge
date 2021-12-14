using System.Collections.Generic;
using System.IO.Ports;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SkyTerm
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ComVisible(true)]
    public class BillPrinterDriver : Driver, INotePrt
    {
        public override void Test()
        {
            throw new System.NotImplementedException();
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