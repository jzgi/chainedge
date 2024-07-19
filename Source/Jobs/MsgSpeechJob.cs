using ChainEdge.Drivers;

namespace ChainEdge.Jobs;

public class MsgSpeechJob : Job
{
    short typ;

    string name;

    string content;

    string tip;

    string oker;

    public override void OnInit()
    {
        Data.Get(nameof(typ), ref typ);
        Data.Get(nameof(name), ref name);
        Data.Get(nameof(content), ref content);
        Data.Get(nameof(tip), ref tip);
        Data.Get(nameof(oker), ref oker);
    }

    public override void Run()
    {
        if (Driver is SpeechDriver drv)
        {
            for (int i = 0; i < 2; i++)
            {
                if (string.IsNullOrEmpty(name))
                {
                    drv.Speak("通知：");
                }
                else
                {
                    drv.Speak(name);
                }
                drv.Speak(content);
            }
            drv.Speak(tip);
        }
    }
}