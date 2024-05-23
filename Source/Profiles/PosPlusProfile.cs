using ChainEdge.Drivers;
using ChainEdge.Jobs;
using ChainFX;

namespace ChainEdge.Profiles;

public class PosPlusProfile : PosProfile, IProxiable
{
    public PosPlusProfile(string name) : base(name)
    {
        CreateDriver<MifareOneDriver>("MCARD");

        CreateDriver<ESCPSerialPrintDriver>("PRINT");
    }


    public override void Downstream(IGateway from, JObj data)
    {
        base.Downstream(from, data);

        //
    }
}