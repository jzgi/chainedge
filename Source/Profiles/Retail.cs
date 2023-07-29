using ChainEdge.Drivers;
using ChainEdge.Features;
using ChainFx;

namespace ChainEdge.Profiles;

public class Retail : Profile
{
    public Retail()
    {
        CreateDriver<ESCPOSSerialPrintDriver>("RECEIPT-A");

        CreateDriver<ESCPOSSerialPrintDriver>("RECEIPT-B");

        CreateDriver<ESCPSerialPrintDriver>("PRINT");

        CreateDriver<CASSerialScaleDriver>("SCALE");

        CreateDriver<ObjectDetectorDriver>("OBJ-DETECT");
    }

    public override int DispatchInput()
    {
        SynthesizerDriver drv = new SynthesizerDriver();
        JObj v = new JObj();

        var job = new Job<ISpeech>(drv, v, (d, x) => { drv.Speak(""); });

        // assign to device
        drv.Add(job);

        return 0;
    }
}