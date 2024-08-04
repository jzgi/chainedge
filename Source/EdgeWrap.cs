using System.Runtime.InteropServices;
using ChainFX;

namespace ChainEdge;

[ClassInterface(ClassInterfaceType.AutoDual)]
[ComVisible(true)]
public class EdgeWrap : IPipe
{
    public string CallGetData(string driverKey, string[] @params)
    {
        return null;
    }

    public void PostData(JObj v)
    {
        // post a message to javascript side
        EdgeApp.Win.PostData(v);
    }

    public string CallDriverToRun(string drvKey, JObj v)
    {
        var drv = EdgeApp.CurrentProfile.GetDriver(drvKey);
        if (drv != null)
        {
            var ret = drv.CallToRun(v);

            if (ret != null)
            {
                return ret.ToString();
            }
        }

        return null;
    }
}