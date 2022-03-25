using System;
using SkyGate.Feature;

namespace SkyGate.Driver
{
    public class DocumentPrinterDriver : Driver, ILabelPrint, INotePrint
    {
        public override void Test()
        {
            throw new NotImplementedException();
        }

        public void printBizlabel()
        {
            throw new NotImplementedException();
        }

        public void PrintBuyReceipt()
        {
            throw new NotImplementedException();
        }

        public void PrintShipList()
        {
            throw new NotImplementedException();
        }
    }
}