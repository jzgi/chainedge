using ChainEdge.Profiles;
using ChainFx;

namespace ChainEdge;

public abstract class Profile : IKeyable<string>
{
    static Map<string, Profile> all = new()
    {
        new RetailPlusProfile("RETAIL-PLUS"),

        new RetailProfile("RETAIL"),

        new WorkstnProfile("WORKSTN"),

        new KioskProfile("KIOSK")
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

    public void TestEveryDriver()
    {
        for (int i = 0; i < drivers.Count; i++)
        {
            var drv = drivers.ValueAt(i);

            drv.Test();
        }
    }

    public Driver GetDriver(string drvKey)
    {
        return drivers[drvKey];
    }

    public Driver GetDriver<T>(string prefix) where T : Driver
    {
        for (int i = 0; i < drivers.Count; i++)
        {
            var v = drivers.ValueAt(i);

            if (v is T)
            {
                return v;
            }
        }
        return null;
    }

    public virtual int Upstream()
    {
        return 0;
    }

    public abstract int Downstream(IGateway from, JObj v);


    public string Key => name;


    public static Profile GetProfile(string name) => all[name];
}