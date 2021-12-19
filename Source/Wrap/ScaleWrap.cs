using System.Runtime.InteropServices;

namespace SkyEdge.Wrap
{
    [ComVisible(true)]
    public class ScaleWrap : WrapBase, IScale
    {
        protected override object GetActiveObject()
        {
            throw new System.NotImplementedException();
        }

        public int Current() => 1;
    }
}