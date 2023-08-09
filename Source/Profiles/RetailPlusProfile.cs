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

    public override int Downstream(IGateway from, JObj jo)
    {
        string[] news = null;
        jo.Get(nameof(news), ref news);

        if (news != null)
        {
            var drv = GetDriver<SpeechDriver>(null);
            drv?.Add(new TextSpeechJob(news));

            return 0;
        }

        string utel = null;
        jo.Get(nameof(utel), ref utel);
        if (utel != null)
        {
            var buy = new Buy();
            buy.Read(jo);
            var drv = GetDriver<ESCPOSSerialPrintDriver>(null);
            drv.Add(new BuyPrintJob(buy));

            return 0;
        }

        // print order
        {
            string name = null;
            jo.Get(nameof(name), ref name);
            string tip = null;
            jo.Get(nameof(tip), ref tip);
            string oker = null;
            jo.Get(nameof(oker), ref oker);

            var drv = GetDriver<SpeechDriver>(null);
            drv?.Add(new TextSpeechJob(name, tip, "播报员：" + oker));
        }

        return 1;
    }
}