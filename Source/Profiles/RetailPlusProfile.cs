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
        if (data.Contains("content"))
        {
            var drv = GetDriver<SpeechDriver>();
            drv?.Add<MsgSpeechJob>(data);
        }
    }
}