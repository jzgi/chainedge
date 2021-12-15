using System;

namespace SkyTerm
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

            MakeDriver<INotePrt, BillPrinterDriver, DocPrinterDriver>("noteprt");

            MakeDriver<ILabelPrt, LabelPrinterDriver>("labelprt");

            MakeDriver<IRecognit, CameraRecognitDriver>("recognit");

            MakeDriver<IDisplay, SubWindowDisplayDriver>("display");

            MakeDriver<ICatalog, MemoryCatalogDriver>("catalog");

            Start();
        }
    }
}