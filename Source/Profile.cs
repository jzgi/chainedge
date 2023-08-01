using ChainEdge.Profiles;
using ChainFx;

namespace ChainEdge;

public abstract class Profile : IKeyable<string>
{
    static Map<string, Profile> all = new()
    {
        new RetailPlus("RETAIL-PLUS"),
        new Retail("RETAIL"),
        new Workstn("WORKSTN"),
        new Kiosk("KIOSK")
    };

    readonly string name;

    readonly Map<string, Driver> drivers = new();


    protected Profile(string name)
    {
        this.name = name;
    }

    public void CreateDriver<D>(string drvKey) where D : Driver, new()
    {
        var drv = new D()
        {
            Key = drvKey
        };
        drivers.Add(drv);
    }

    public virtual int DispatchInput()
    {
        return 0;
    }

    public virtual int DispatchOutput()
    {
        return 0;
    }


    public string Key => name;


    public static Profile GetProfile(string name) => all[name];
}