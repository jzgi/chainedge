using ChainEdge.Drivers;
using ChainEdge.Jobs;
using ChainFx;

namespace ChainEdge.Profiles;

public class RetailPlusProfile : Profile
{
    public RetailPlusProfile(string name) : base(name)
    {
        CreateDriver<ESCPOSSerialPrintDriver>("RECEIPT");

        CreateDriver<ESCPSerialPrintDriver>("PRINT");

        CreateDriver<CASSerialScaleDriver>("SCALE");

        CreateDriver<MifareOneDriver>("MCARD");

        CreateDriver<ObjectDetectorDriver>("OBJ-DETECT");

        CreateDriver<SpeechDriver>("SPEECH");

        CreateDriver<LedBoardDriver>("LEDBRD");

        CreateDriver<GiantLedBoardDriver>("GIANT-LEDBRD");
    }

    public override int Upstream()
    {
        SpeechDriver drv = new SpeechDriver();
        JObj v = new JObj();

        // var job = new Event<ISpeech>(drv, v, (d, x) => { drv.Speak(""); });

        // assign to device
        // drv.Add(job);

        return 0;
    }

    public override int Downstream(IGateway from, JObj v)
    {
        string[] news = null;
        v.Get(nameof(news), ref news);

        if (news != null)
        {
            var drv = GetDriver<SpeechDriver>(null);
            drv?.Add(new TextSpeechJob(news));

            return 0;
        }

        string[] notes = null;
        v.Get(nameof(notes), ref notes);
        if (notes != null)
        {
            var drv = GetDriver<SpeechDriver>(null);
            drv?.Add(new TextSpeechJob(notes));

            return 0;
        }

        // print order
        {
            var buy = new Buy();
            buy.Read(v);

            var drv = GetDriver<ESCPOSSerialPrintDriver>(null);
            drv.Add(new BuyPrintJob(buy));
        }

        return 1;
    }
}