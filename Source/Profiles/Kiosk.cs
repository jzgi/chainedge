using ChainEdge.Drivers;
using ChainFx;

namespace ChainEdge.Profiles;

public class Kiosk : Profile
{
    public Kiosk(string name) : base(name)
    {
        CreateDriver<ESCPOSSerialPrintDriver>("RECEIPT");

        CreateDriver<MifareOneDriver>("MCARD");

        CreateDriver<SpeechDriver>("OBJ-DETECT");
    }

    public override int Upstream()
    {
        return base.Upstream();
    }

    public override int Downstream(IEventPlay from, JObj v)
    {
        throw new System.NotImplementedException();
    }
}