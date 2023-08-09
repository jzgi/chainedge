using System;
using System.Runtime.InteropServices;
using ChainFx;

namespace ChainEdge;

[ClassInterface(ClassInterfaceType.AutoDual)]
[ComVisible(true)]
public class EdgeHost : IGateway
{
    public string CallGetData(string driverKey, string[] @params)
    {
        return null;
    }

    public void Enqueue(JObj jo)
    {
        throw new NotImplementedException();
    }

    public void AddDownstream(JObj v)
    {
        EdgeApp.CurrentProfile.Downstream(this, v);
    }


    public bool IsDriverCallable(string drvKey)
    {
        var drv = EdgeApp.CurrentProfile.GetDriver(drvKey);
        if (drv != null)
        {
            return drv.IsCallable;
        }
        return false;
    }

    public string CallDriverToDo(string drvKey, JObj v)
    {
        var drv = EdgeApp.CurrentProfile.GetDriver(drvKey);
        if (drv != null)
        {
            var ret = drv.CallToDo(v);

            if (ret != null)
            {
                return ret.ToString();
            }
        }

        return null;
    }
}