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

    public override void HandUp(Driver from, JObj data)
    {
    }

    public override void HandDown(IGateway from, JObj data)
    {
    }
}