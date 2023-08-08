using ChainEdge.Drivers;

namespace ChainEdge.Jobs;

public class TextDisplayJob : Job<GiantLedBoardDriver>
{
    readonly string[] texts;

    public TextDisplayJob(string[] texts)
    {
        this.texts = texts;
    }

    protected internal override void Do()
    {
    }
}