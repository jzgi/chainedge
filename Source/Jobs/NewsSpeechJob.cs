using ChainEdge.Drivers;

namespace ChainEdge.Jobs;

public class NewsSpeechJob : Job
{
    string[] news;

    protected internal override void OnInitialize()
    {
        Data.Get(nameof(news), ref news);
    }

    protected internal override void Perform()
    {
        while (Repeats > 0)
        {
            if (Driver is SpeechDriver drv)
            {
                if (news.Length >= 2)
                {
                    drv.Speak("新单通知：");
                }
                foreach (var v in news)
                {
                    if (news.Length == 1)
                    {
                        drv.Speak(v + "有新单");
                    }
                    else
                    {
                        drv.Speak(v);
                    }
                }
            }
            Repeats--;
        }
    }

    public override string ToString()
    {
        return news[0];
    }
}