using System;
using ChainEdge.Features;

namespace ChainEdge.Drivers
{
    public class A4PrintDriver : Driver, ILabelPrint, IBillPrint
    {
        public override void Test()
        {
            throw new NotImplementedException();
        }

        public void printBizlabel()
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