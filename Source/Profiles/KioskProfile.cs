using ChainEdge.Drivers;
using ChainFx;

namespace ChainEdge.Profiles;

public class KioskProfile : Profile
{
    public KioskProfile(string name) : base(name)
    {
        CreateDriver<ESCPOSSerialPrintDriver>("RECEIPT");

        CreateDriver<MifareOneDriver>("MCARD");

        CreateDriver<SpeechDriver>("OBJ-DETECT");
    }

    public override int Upstream()
    {
        return base.Upstream();
    }

    public override int Downstream(IGateway from, JObj v)
    {
        throw new System.NotImplementedException();
    }
}