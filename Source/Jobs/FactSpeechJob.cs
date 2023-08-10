using ChainEdge.Drivers;

namespace ChainEdge.Jobs;

public class FactSpeechJob : Job
{
    string name;

    string tip;

    string oker;

    protected internal override void Initialize()
    {
        Data.Get(nameof(name), ref name);

        Data.Get(nameof(tip), ref tip);

        Data.Get(nameof(oker), ref oker);
    }

    protected internal override void Perform()
    {
        if (Driver is SpeechDriver drv)
        {
            drv.Speak(name);
            drv.Speak(tip);
            drv.Speak("播报员：" + oker);
        }
    }
}