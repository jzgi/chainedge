using System;

namespace SkyEdge
{
    /// <summary>
    /// the entry point for the application.
    /// </summary>
    public class SkyTermApp : FrameworkApp
    {
        [STAThread]
        public static void Main()
        {
            MakeDriver<IScale, AcpiScaleDriver>("scale");

            MakeDriver<INotePrint, BillPrinterDriver, DocPrinterDriver>("noteprt");

            MakeDriver<ILabelPrint, LabelPrinterDriver>("labelprt");

            MakeDriver<IRecognition, CameraRecognizerDriver>("recog");

            MakeDriver<IDisplay, SubWindowDisplayDriver>("display");

            MakeDriver<ICatalog, MemoryCatalogDriver>("catalog");

            MakeDriver<IHistory, FileHistoryDriver>("history");

            Start();
        }
    }
}