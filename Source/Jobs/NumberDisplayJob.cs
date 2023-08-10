using ChainEdge.Drivers;

namespace ChainEdge.Jobs;

public class NumberDisplayJob : Job
{
    readonly string[] texts;

    public NumberDisplayJob(string[] texts)
    {
        this.texts = texts;
    }

    protected internal override void Initialize()
    {
        throw new System.NotImplementedException();
    }

    protected internal override void Perform()
    {
    }
}