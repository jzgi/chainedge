using ChainEdge.Drivers;
using ChainEdge.Jobs;
using ChainFx;

namespace ChainEdge.Profiles;

public class RetailPlusProfile : RetailProfile
{
    public RetailPlusProfile(string name) : base(name)
    {
        // CreateDriver<ESCPSerialPrintDriver>("PRINT");
        //
        // CreateDriver<MifareOneDriver>("MCARD");
        //
        // CreateDriver<SpeechDriver>("SPEECH");
        //
        // CreateDriver<GiantLedBoardDriver>("GIANT-LEDBRD");
    }


    public override void Dispatch(IGateway from, JObj data)
    {
        base.Dispatch(from, data);

        if (data.Contains("news"))
        {
            var drv = GetDriver<SpeechDriver>();
            drv?.Add<NewsSpeechJob>(data, 3);
        }

        // print order
        if (data.Contains("num"))
        {
            var drv = GetDriver<SpeechDriver>();
            drv?.Add<FactSpeechJob>(data);
        }
    }
}