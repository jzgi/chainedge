using System;
using SkyEdge.Driver;

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
            MakeDriver<IScale, AcpiScaleDriver>("scale");

            MakeDriver<INotePrint, BillPrinterDriver, DocPrinterDriver>("noteprt");

            MakeDriver<ILabelPrint, LabelPrinterDriver>("labelprt");

            MakeDriver<IRecognition, CameraRecognizerDriver>("recog");

            MakeDriver<IDisplay, SubWindowDisplayDriver>("display");

            MakeDriver<ICatalog, MemoryCatalogDriver>("catalog");

            MakeDriver<IJournal, FileJournalDriver>("journal");

            Start();
        }
    }
}