using System;
using ChainEdge.Drivers;
using ChainEdge.Jobs;
using ChainFX;

namespace ChainEdge.Profiles;

public class PosProfile : Profile
{
    public PosProfile(string name) : base(name)
    {
        CreateDriver<ESCPOSSerialPrintDriver>("RECEIPT");

        CreateDriver<SpeechDriver>("SPEECH");

        CreateDriver<BarcodeScannerDriver>("BARCODE-SCAN");
    }

    public override void Upstream(Driver from, JObj data)
    {
        if (from.Key == "SCALE")
        {
            EdgeApp.Wrap.AddData(data);
        }
    }

    DateTime last;

    public override void Downstream(IGateway from, JObj data)
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
            drv?.Add<NewOrderPrintJob>(data);
        }
    }
}