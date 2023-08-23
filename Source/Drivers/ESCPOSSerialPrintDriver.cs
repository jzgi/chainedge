using System;
using System.IO.Ports;

namespace ChainEdge.Drivers
{
    public class ESCPOSSerialPrintDriver : Driver
    {
        readonly SerialPort port = new()
        {
            BaudRate = 19200,
            Parity = Parity.None,
            DataBits = 8,
            StopBits = StopBits.One
        };


        const string ESC = "\u001B";
        const string GS = "\u001D";
        const string InitializePrinter = ESC + "@";
        const string BoldOn = ESC + "E" + "\u0001";
        const string BoldOff = ESC + "E" + "\0";
        const string DoubleOn = GS + "!" + "\u0011";

        const string DoubleOff = GS + "!" + "\0";
        // static readonly string[] names = { "COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8", "COM9" };

        public override void Reset()
        {
            var names = SerialPort.GetPortNames();
            foreach (var name in names)
            {
                port.PortName = name;
                try
                {
                    port.Open();

                    // port.Write(InitializePrinter);
                    // port.WriteLine("Here is some normal text.");
                    // port.WriteLine(BoldOn + "Here is some bold text." + BoldOff);
                    // port.WriteLine(DoubleOn + "Here is some large text." + DoubleOff);

                    status = STU_READY;
                    
                    port.Close();
                }
                catch (Exception e)
                {
                }
            }
        }

        public override string Label => "票据打印";


        public ESCPOSSerialPrintDriver HT()
        {
            port.WriteLine("\t");

            return this;
        }

        public ESCPOSSerialPrintDriver T(string v)
        {
            port.WriteLine(v);

            return this;
        }

        public ESCPOSSerialPrintDriver T(decimal v)
        {
            port.WriteLine(v.ToString());

            return this;
        }
    }
}