using System;

namespace ChainEdge.Drivers
{
    public class ESCPSerialPrintDriver : Driver
    {
        public override void Test()
        {
        }

        public override string Label => "行打印";

        public void printBizlabel()
        {
        }

        public void PrintTitle(string v)
        {
        }

        public void PrintRow(short idx, string name, decimal price, short qty)
        {
        }

        public void PrintBottomLn()
        {
        }
    }
}