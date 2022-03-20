using System;

namespace SkyGate.Driver
{
    public class DocumentPrinterDriver : DriverBase, ILabelPrint, INotePrint
    {
        public override void Test()
        {
            throw new NotImplementedException();
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
    }
}