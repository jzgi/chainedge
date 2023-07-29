using System.Collections.Concurrent;
using ChainEdge.Profiles;
using ChainFx;

namespace ChainEdge;

public abstract class Profile : IKeyable<string>
{
    static Map<string, Profile> all = new()
    {
        new Retail(),
        new Warehouse()
    };

    readonly Map<string, Driver> drivers = new();

    public void CreateDriver<D>(string name) where D : Driver, new()
    {
        var drv = new D();
        drivers.Add(drv);
    }
    private string key;

    public virtual int DispatchInput()
    {
        return 0;
    }

    public virtual int DispatchOutput()
    {
        return 0;
    }


    public string Key => key;
    
}