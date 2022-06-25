using System;
using CoEdge.Features;

namespace CoEdge.Drivers
{
    public class DocPrinterDriver : Driver, ILabelPrint, IBillPrint
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