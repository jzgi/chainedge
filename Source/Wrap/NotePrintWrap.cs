using System;
using System.Runtime.InteropServices;

namespace SkyEdge.Wrap
{
    [ComVisible(true)]
    public class NotePrintWrap : WrapBase, INotePrint
    {
        protected override object GetActiveObject()
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