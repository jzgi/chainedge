using ChainEdge.Profiles;
using ChainFX;

namespace ChainEdge;

public abstract class Profile : IKeyable<string>
{
    static Map<string, Profile> all = new()
    {
        new PosPlusProfile("POS-PLUS"),

        new PosProfile("POS"),

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

    public Map<string, Driver> Drivers => drivers;

    public void Start()
    {
        for (int i = 0; i < drivers.Count; i++)
        {
            var drv = drivers.ValueAt(i);

            drv.StartToRun();
        }
    }

    public Driver GetDriver(string drvKey)
    {
        return drivers[drvKey];
    }

    public T GetDriver<T>(string prefix = null) where T : Driver
    {
        for (int i = 0; i < drivers.Count; i++)
        {
            var v = drivers.ValueAt(i);

            if (prefix != null && !v.Key.StartsWith(prefix)) continue;

            if (v is T drv)
            {
                return drv;
            }
        }
        return null;
    }

    public abstract void Upstream(Driver from, JObj data);

    public abstract void Downstream(IGateway from, JObj data);


    public string Key => name;

    public static Profile GetProfile(string name) => all[name];
}