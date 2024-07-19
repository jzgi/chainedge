using ChainEdge.Profiles;
using ChainFX;

namespace ChainEdge;

public abstract class Profile : IKeyable<string>
{
    public static readonly Map<string, Profile> All = new()
    {
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

    public void CreateDriver<D>(string drvKey, object state = null) where D : Driver, new()
    {
        var drv = new D
        {
            Key = drvKey,
            Profile = this,
        };

        // init the instance
        drv.OnCreate(state);

        // add to the collection
        drivers.Add(drv);
    }

    public Map<string, Driver> Drivers => drivers;


    /// <summary>
    /// Starts all drivers defined & contained in this profile.
    /// </summary>
    public void StartAll()
    {
        for (int i = 0; i < drivers.Count; i++)
        {
            var drv = drivers.ValueAt(i);

            drv.Start();
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

    public abstract void Upward(Driver from, JObj dat);

    public abstract void Downward(IGateway from, JObj dat);

    public string Key => name;
}