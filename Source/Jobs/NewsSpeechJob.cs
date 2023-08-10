using ChainEdge.Drivers;

namespace ChainEdge.Jobs;

public class NewsSpeechJob : Job
{
    string[] news;

    protected internal override void Initialize()
    {
        Data.Get(nameof(news), ref news);
    }

    protected internal override void Perform()
    {
        if (Driver is SpeechDriver drv)
        {
            foreach (var v in news)
            {
                drv.Speak(v);
            }
        }
    }
}