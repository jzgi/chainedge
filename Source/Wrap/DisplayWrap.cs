using System.Runtime.InteropServices;

namespace SkyEdge.Wrap
{
    [ComVisible(true)]
    public class DisplayWrap : WrapBase, IDisplay
    {
        protected override object GetActiveObject()
        {
            throw new System.NotImplementedException();
        }

        public void open(string uri)
        {
        }
    }
}