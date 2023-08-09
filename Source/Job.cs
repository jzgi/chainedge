namespace ChainEdge;

public abstract class Job
{
    short status;

    public short Status => status;

    protected internal abstract void Do();
}

/// <summary>
/// An output job that takes event content and operates on device driver.
/// </summary>
public abstract class Job<D> : Job where D : Driver
{
    protected internal override void Do()
    {
    }

    public D Driver { get; internal set; }
}