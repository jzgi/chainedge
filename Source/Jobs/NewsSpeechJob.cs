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
        if (news == null || news.Length == 0) return;

        if (Driver is SpeechDriver drv)
        {
            while (Repeat > 0)
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

                if (Repeat == 1)
                {
                    drv.Speak("请及时处理");
                }

                Repeat--;
            }
        }
    }

    public override string ToString()
    {
        return news[0];
    }
}