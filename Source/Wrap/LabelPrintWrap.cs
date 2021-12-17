using System;
using System.Runtime.InteropServices;

namespace SkyEdge.Wrap
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ComVisible(true)]
    public class LabelPrintWrap : WrapBase<ILabelPrint>, ILabelPrint
    {
        public void printBizlabel()
        {
            throw new NotImplementedException();
        }
    }
}