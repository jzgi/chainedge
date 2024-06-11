using ChainFX;

namespace ChainEdge.Profiles;

public class PosPlusProfile : PosProfile, IProxiable
{
    public PosPlusProfile(string name) : base(name)
    {
        CreateDriver<MifareOneDriver>("MCARD");
    }


    public override void HandDown(IGateway from, JObj data)
    {
        base.HandDown(from, data);

        //
    }
}