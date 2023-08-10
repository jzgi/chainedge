using ChainEdge.Drivers;
using ChainEdge.Jobs;
using ChainFx;

namespace ChainEdge.Profiles;

public class RetailPlusProfile : RetailProfile
{
    public RetailPlusProfile(string name) : base(name)
    {
        CreateDriver<ESCPSerialPrintDriver>("PRINT");

        CreateDriver<MifareOneDriver>("MCARD");

        CreateDriver<SpeechDriver>("SPEECH");

        CreateDriver<GiantLedBoardDriver>("GIANT-LEDBRD");
    }


    public override void DispatchDown(IGateway from, JObj data)
    {
        base.DispatchDown(from, data);

        if (data.Contains("news"))
        {
            var drv = GetDriver<SpeechDriver>();
            drv?.Add<NewsSpeechJob>(data);
        }

        // print order
        if (data.Contains("num"))
        {
            var drv = GetDriver<SpeechDriver>();
            drv?.Add<FactSpeechJob>(data);
        }
    }
}