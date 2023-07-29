using ChainEdge.Drivers;
using ChainEdge.Features;
using ChainFx;

namespace ChainEdge.Profiles;

public class Retail : Profile
{
    public Retail() : base("RETAIL")
    {
        CreateDriver<ESCPOSSerialPrintDriver>("RECEIPT-A");

        CreateDriver<ESCPOSSerialPrintDriver>("RECEIPT-B");

        CreateDriver<ESCPSerialPrintDriver>("PRINT");

        CreateDriver<CASSerialScaleDriver>("SCALE");

        CreateDriver<MifareOneDriver>("M1");

        CreateDriver<ObjectDetectorDriver>("OBJ-DETECT");

        CreateDriver<SpeechDriver>("OBJ-DETECT");
    }

    public override int DispatchInput()
    {
        SpeechDriver drv = new SpeechDriver();
        JObj v = new JObj();

        var job = new Job<ISpeech>(drv, v, (d, x) => { drv.Speak(""); });

        // assign to device
        drv.Add(job);

        return 0;
    }
}