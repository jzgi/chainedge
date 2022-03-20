using System;
using SkyGate.Driver;
using SkyGate.Wrap;

namespace SkyGate
{
    /// <summary>
    /// The entry point for the application.
    /// </summary>
    public class SkyEdgeApp : ApplicationBase
    {
        [STAThread]
        public static void Main()
        {
            MakeDriver<IScale, ScaleWrap, AcpiScaleDriver>("scale");

            MakeDriver<INotePrint, NotePrintWrap, BillPrinterDriver, DocumentPrinterDriver>("noteprt");

            MakeDriver<ILabelPrint, LabelPrintWrap, LabelPrinterDriver>("labelprt");

            MakeDriver<IRecognize, RecognizeWrap, CameraRecognizerDriver>("recognize");

            MakeDriver<IDisplay, DisplayWrap, SideWindowDisplayDriver>("display");

            MakeDriver<ICatalog, CatalogWrap, MemoryCatalogDriver>("catalog");

            MakeDriver<IJournal, JournalWrap, FileJournalDriver>("journal");

            Start();
        }
    }
}