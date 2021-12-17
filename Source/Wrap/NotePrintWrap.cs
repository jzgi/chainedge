using System;
using System.Runtime.InteropServices;

namespace SkyEdge.Wrap
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ComVisible(true)]
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