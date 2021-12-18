using System;
using System.Runtime.InteropServices;

namespace SkyEdge.Wrap
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ComVisible(true)]
    public class LabelPrintWrap : WrapBase, ILabelPrint
    {
        protected override object GetActiveObject()
        {
            throw new NotImplementedException();
        }

        public void printBizlabel()
        {
            throw new NotImplementedException();
        }
    }
}