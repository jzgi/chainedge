using System;
using SkyEdge.Driver;
using SkyEdge.Wrap;

namespace SkyEdge
{
    /// <summary>
    /// The entry point for the application.
    /// </summary>
    public class SkyTermApp : ApplicationExt
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