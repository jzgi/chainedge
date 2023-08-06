using ChainEdge.Drivers;
using ChainEdge.Features;
using ChainFx;

namespace ChainEdge.Profiles;

public class Retail : Profile
{
    public Retail(string name) : base(name)
    {
        CreateDriver<ESCPOSSerialPrintDriver>("RECEIPT");

        CreateDriver<CASSerialScaleDriver>("SCALE");

        CreateDriver<ObjectDetectorDriver>("OBJ-DETECT");

        CreateDriver<SpeechDriver>("SPEECH");

        CreateDriver<LedBoardDriver>("LEDBRD");
    }

    public override int Upstream()
    {
        SpeechDriver drv = new SpeechDriver();
        JObj v = new JObj();

        var job = new Event<ISpeech>(drv, v, (d, x) => { drv.Speak(""); });

        // assign to device
        drv.Add(job);

        return 0;
    }

    public override int Downstream(IEventPlay from, JObj v)
    {
        throw new System.NotImplementedException();
    }
}