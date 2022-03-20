using System;
using System.Runtime.InteropServices;

namespace SkyGate.Wrap
{
    [ComVisible(true)]
    public class RecognizeWrap : WrapBase, IRecognize
    {
        protected override object GetActiveObject()
        {
            throw new NotImplementedException();
        }

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