using System.Runtime.InteropServices;
using ChainFX;

namespace ChainEdge;

[ClassInterface(ClassInterfaceType.AutoDual)]
[ComVisible(true)]
public class EdgeWrap : IGateway
{
    public string CallGetData(string driverKey, string[] @params)
    {
        return null;
    }

    public void PostData(JObj v)
    {
        // post a message to javascript side
        EdgeApplication.Win.PostData(v);
    }

    public string CallDriverPerform(string drvKey, JObj v)
    {
        var drv = EdgeApplication.CurrentProfile.GetDriver(drvKey);
        if (drv != null)
        {
            var ret = drv.Perform(v);

            if (ret != null)
            {
                return ret.ToString();
            }
        }

        return null;
    }
}