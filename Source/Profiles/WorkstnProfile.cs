using ChainEdge.Drivers;
using ChainFX;

namespace ChainEdge.Profiles;

public class WorkstnProfile : Profile
{
    public WorkstnProfile(string name) : base(name)
    {
        CreateDriver<ESCPOSSerialPrintDriver>("RECEIPT");

        CreateDriver<ESCPSerialPrintDriver>("PRINT");

        CreateDriver<MifareOneDriver>("MCARD");

        CreateDriver<SpeechDriver>("SPEECH");
    }

    public override void Upward(Driver from, JObj dat)
    {
    }

    public override void Downward(IGateway from, JObj dat)
    {
    }
}