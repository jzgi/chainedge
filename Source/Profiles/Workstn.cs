using ChainEdge.Drivers;
using ChainFx;

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

    public override int Upstream()
    {
        return base.Upstream();
    }

    public override int Downstream(IEventPlay from, JObj v)
    {
        throw new System.NotImplementedException();
    }
}