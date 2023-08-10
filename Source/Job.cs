using ChainFx;

namespace ChainEdge;

/// <summary>
/// An output job that takes event content and operates on device driver.
/// </summary>
public abstract class Job
{
    short status;

    public short Status => status;

    public JObj Data { get; set; }

    public int Repeats { get; set; }

    public Driver Driver { get; internal set; }

    protected internal abstract void Initialize();

    protected internal abstract void Perform();
}