using System;

namespace SkyEdge.Wrap
{
    public class NotePrintWrap : WrapBase<INotePrint>, INotePrint
    {
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