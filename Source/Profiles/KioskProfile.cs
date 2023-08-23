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

    public override void Dispatch(Driver from, JObj data)
    {
    }

    public override void Dispatch(IGateway from, JObj data)
    {
        throw new System.NotImplementedException();
    }
}