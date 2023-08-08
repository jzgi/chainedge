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

    public void Add(JObj evt)
    {
        throw new NotImplementedException();
    }
}