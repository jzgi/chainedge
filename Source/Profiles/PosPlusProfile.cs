using ChainEdge.Drivers;
using ChainFX;

namespace ChainEdge.Profiles;

public class PosPlusProfile : PosProfile, IProxiable
{
    public PosPlusProfile(string name) : base(name)
    {
        CreateDriver<ESCPOSSerialPrintDriver>("RECEIPT-IN", 9600); // inner printer

        CreateDriver<CASSerialScaleDriver>("SCALE");
    }


    public override void DispatchDown(IGateway from, JObj data)
    {
        base.DispatchDown(from, data);

        //
    }
}