using System;
using ChainEdge.Drivers;
using ChainEdge.Jobs;
using ChainFX;

namespace ChainEdge.Profiles;

public class PosProfile : Profile
{
    public PosProfile(string name) : base(name)
    {
        CreateDriver<SpeechDriver>("SPEECH");

        CreateDriver<BarcodeScannerDriver>("BARCODE");

        CreateDriver<MifareOneDriver>("MCARD");

        CreateDriver<ESCPOSSerialPrintDriver>("RECEIPT", 19200); // external printer
    }

    public override void DispatchUp(Driver from, JObj data)
    {
        if (from.Key == "SCALE")
        {
            EdgeApplication.Wrap.PostData(data);
        }
    }

    DateTime last;

    public override void DispatchDown(IGateway from, JObj data)
    {
        DateTime created = data[nameof(created)];

        if (created - last > TimeSpan.FromMinutes(1))
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