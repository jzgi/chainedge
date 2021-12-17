using System.Runtime.InteropServices;

namespace SkyEdge.Wrap
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ComVisible(true)]
    public class DisplayWrap : WrapBase<IDisplay>, IDisplay
    {
        public void open(string uri)
        {
            GetActive().open(uri);
        }
    }
}