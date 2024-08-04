using System;
using ChainEdge.Drivers;
using ChainEdge.Jobs;
using ChainEdge.Source.Drivers;
using ChainFX;

namespace ChainEdge.Profiles;

public class PosProfile : Profile
{
    public PosProfile(string name) : base(name)
    {
        CreateDriver<VideoCallDriver>("VCALL");

        CreateDriver<ESCPOSSerialPrintDriver>("RECEIPT"); // built-in / external external printer

        CreateDriver<CASSerialScaleDriver>("SCALE");

        CreateDriver<PlayDriver>("PLAY");
        
        CreateDriver<MicroLedDriver>("MLED");

        CreateDriver<SpeechDriver>("SPEECH");
    }

    public override void Upward(Driver from, JObj dat)
    {
        if (from.Key == "SCALE")
        {
            EdgeApp.Win.PostData(dat);
        }
    }

    DateTime last;

    public override void Downward(IPipe from, JObj dat)
    {
        DateTime created = dat[nameof(created)];

        if (from is EdgeConnect)
        {
            var drv = GetDriver<SpeechDriver>();
            drv?.AddJob<NewOrderSpeechJob>(dat);
        }

        last = created;

        // print order
        if (dat.Contains(nameof(created)))
        {
            var drv = GetDriver<ESCPOSSerialPrintDriver>();
            drv?.AddJob<OrderPrintJob>(dat);
        }
    }
}