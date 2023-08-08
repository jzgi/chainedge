using ChainEdge.Drivers;

namespace ChainEdge.Jobs;

public class TextSpeechJob : Job<SpeechDriver>
{
    readonly string[] texts;

    public TextSpeechJob(params string[] texts)
    {
        this.texts = texts;
    }

    protected internal override void Do()
    {
        var drv = Driver;

        foreach (var note in texts)
        {
            drv.Speak(note);
        }
    }
}