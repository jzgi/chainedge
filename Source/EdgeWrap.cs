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

    public void AddData(JObj v)
    {
        // post a message to javascript side
        EdgeApplication.Win.PostMessage(v);
    }

    public string CallDriverPerform(string drvKey, JObj v)
    {
        var drv = EdgeApplication.Profile.GetDriver(drvKey);
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