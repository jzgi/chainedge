using ChainEdge.Drivers;

namespace ChainEdge.Profiles;

public class Kiosk : Profile
{
    public Kiosk(string name) : base(name)
    {
        CreateDriver<ESCPOSSerialPrintDriver>("RECEIPT");

        CreateDriver<MifareOneDriver>("MCARD");

        CreateDriver<SpeechDriver>("OBJ-DETECT");
    }
}