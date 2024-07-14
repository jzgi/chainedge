using System;
using ChainEdge.Drivers;
using ChainEdge.Jobs;
using ChainFX;

namespace ChainEdge.Profiles;

public class PosProfile : Profile
{
    public PosProfile(string name) : base(name)
    {
        CreateDriver<ESCPOSSerialPrintDriver>("RECEIPT"); // built-in / external external printer

        CreateDriver<CASSerialScaleDriver>("SCALE");

        CreateDriver<SpeechDriver>("SPEECH");

        CreateDriver<BarcodeScannerDriver>("BARCODE");
    }

    public override void DispatchUp(Driver from, JObj data)
    {
        if (from.Key == "SCALE")
        {
            EdgeApplication.Win.PostData(data);
        }
    }

    DateTime last;

    public override void DispatchDown(IGateway from, JObj data)
    {
        DateTime created = data[nameof(created)];

        if (from is EdgeConnector)
        {
            var drv = GetDriver<SpeechDriver>();
            drv?.Add<NewOrderSpeechJob>(data);
        }

        last = created;

        // print order
        if (data.Contains(nameof(created)))
        {
            var drv = GetDriver<ESCPOSSerialPrintDriver>();
            drv?.Add<OrderPrintJob>(data);
        }
    }
}