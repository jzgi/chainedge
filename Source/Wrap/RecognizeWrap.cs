using System;
using System.Runtime.InteropServices;

namespace SkyEdge.Wrap
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ComVisible(true)]
    public class RecognizeWrap : WrapBase<IRecognize>, IRecognize
    {
        public int getItemIdByScan()
        {
            throw new NotImplementedException();
        }

        public int getNumberByScan()
        {
            throw new NotImplementedException();
        }
    }
}