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

    public override void Upward(Driver from, JObj dat)
    {
    }

    public override void Downward(IGateway from, JObj dat)
    {
        throw new System.NotImplementedException();
    }
}