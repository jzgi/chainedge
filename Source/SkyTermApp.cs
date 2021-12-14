using System;

namespace SkyTerm
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public class SkyTermApp : FrameworkApp
    {
        [STAThread]
        public static void Main()
        {
            MakeDriver<IScale, AcpiScaleDriver>("scale");

            MakeDriver<INotePrt, BillPrinterDriver, DocPrinterDriver>("noteprt");

            MakeDriver<ILabelPrt, LabelPrinterDriver>("labelprt");

            MakeDriver<ISubView, SubViewDriver>("subview");

            Start();
        }
    }
}