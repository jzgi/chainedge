using ChainEdge.Drivers;

namespace ChainEdge.Profiles;

public class Workstn : Profile
{
    public Workstn(string name) : base(name)
    {
        CreateDriver<ESCPOSSerialPrintDriver>("RECEIPT");

        CreateDriver<ESCPSerialPrintDriver>("PRINT");

        CreateDriver<MifareOneDriver>("MCARD");

        CreateDriver<SpeechDriver>("SPEECH");
    }
}