using ChainEdge.Drivers;
using ChainFX;

namespace ChainEdge.Profiles;

public class KioskProfile : Profile
{
    public KioskProfile(string name) : base(name)
    {
        CreateDriver<ESCPOSSerialPrintDriver>("RECEIPT");

        CreateDriver<MifareOneDriver>("MCARD");

        CreateDriver<SpeechDriver>("OBJ-DETECT");
    }

    public override void Upstream(Driver from, JObj data)
    {
    }

    public override void Downstream(IGateway from, JObj data)
    {
        throw new System.NotImplementedException();
    }
}