using ChainEdge.Drivers;
using ChainEdge.Jobs;
using ChainFx;

namespace ChainEdge.Profiles;

public class RetailProfile : Profile
{
    public RetailProfile(string name) : base(name)
    {
        CreateDriver<ESCPOSSerialPrintDriver>("RECEIPT");

        // CreateDriver<CASSerialScaleDriver>("SCALE");

        // CreateDriver<ObjectDetectorDriver>("OBJ-DETECT");
        //
        // CreateDriver<LedBoardDriver>("LEDBRD");
    }

    public override void Dispatch(Driver from, JObj data)
    {
        if (from.Key == "SCALE")
        {
            EdgeApp.Wrap.SubmitData(data);
        }
    }

    public override void Dispatch(IGateway from, JObj data)
    {
        if (data.Contains("utel"))
        {
            var drv = GetDriver<ESCPOSSerialPrintDriver>(null);
            drv.Add<BuyPrintJob>(data);
        }
    }
}