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

    public override void DispatchUp(Driver from, JObj data)
    {
    }

    public override void DispatchDown(IGateway from, JObj data)
    {
        throw new System.NotImplementedException();
    }
}