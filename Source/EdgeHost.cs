using System;
using System.Runtime.InteropServices;

namespace ChainEdge;

[ClassInterface(ClassInterfaceType.AutoDual)]
[ComVisible(true)]
public class EdgeHost : IEventPlay
{
    public string CallGetData(string driverKey, string[] @params)
    {
        return null;
    }

    public void Add(Event v)
    {
        throw new NotImplementedException();
    }
}