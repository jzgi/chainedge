using System;
using System.Collections.Generic;
using System.Windows;
using SkyGate.Driver;
using SkyGate.Wrap;

namespace SkyGate
{
    /// <summary>
    /// The entry point for the application.
    /// </summary>
    public class SkyGateApp : ApplicationBase
    {
        static readonly Dictionary<Type, Driver.Driver> map = new Dictionary<Type, Driver.Driver>();

        // internal static readonly Dictionary<string, Profile> features = new Dictionary<string, WrapBase>();

        static readonly ApplicationLogger logger;

        // logging level
        static int logging;


        static SkyGateApp()
        {
            // file-based logger
            logging = 3;
            var logfile = DateTime.Now.ToString("yyyyMM") + ".log";
            logger = new ApplicationLogger(logfile)
            {
                Level = logging
            };
        }

        public static void MakeDriver<F, W, D>(string name) where F : IFeature where W : WrapBase, F, new() where D : Driver.Driver, F, new()
        {
            var typ = typeof(D);

            if (!map.TryGetValue(typ, out var drv))
            {
                drv = new D();
                map.Add(typ, drv);
            }

            if (!features.TryGetValue(name, out var wrap))
            {
                wrap = new W();
                wrap.Add((D) drv);
                features.Add(name, wrap);
            }
            else
            {
                wrap.Add((D) drv);
            }
        }

        public static void MakeDriver<F, W, D1, D2>(string name) where F : IFeature where W : WrapBase, F, new() where D1 : Driver.Driver, F, new() where D2 : Driver.Driver, F, new()
        {
            MakeDriver<F, W, D1>(name);
            MakeDriver<F, W, D2>(name);
        }

        public static void MakeDriver<F, W, D1, D2, D3>(string name) where F : IFeature where W : WrapBase, F, new() where D1 : Driver.Driver, F, new() where D2 : Driver.Driver, F, new() where D3 : Driver.Driver, F, new()
        {
            MakeDriver<F, W, D1>(name);
            MakeDriver<F, W, D2>(name);
            MakeDriver<F, W, D3>(name);
        }

        public static void MakeDriver<F, W, D1, D2, D3, D4>(string name) where F : IFeature where W : WrapBase, F, new() where D1 : Driver.Driver, F, new() where D2 : Driver.Driver, F, new() where D3 : Driver.Driver, F, new() where D4 : Driver.Driver, F, new()
        {
            MakeDriver<F, W, D1>(name);
            MakeDriver<F, W, D2>(name);
            MakeDriver<F, W, D3>(name);
            MakeDriver<F, W, D4>(name);
        }

        protected static void Start()
        {
            var win = new MainWindow()
            {
                Title = "SkyGate",
                WindowStyle = WindowStyle.SingleBorderWindow,
                WindowState = WindowState.Maximized
            };
            var app = new ApplicationBase()
            {
                MainWindow = win,
                ShutdownMode = ShutdownMode.OnMainWindowClose,
            };
            win.Show();
            app.Run();
        }

        [STAThread]
        public static void Main()
        {
            MakeDriver<IScaleFeature, ScaleWrap, AcpiScaleDriver>("scale");

            MakeDriver<INotePrint, NotePrintWrap, BillPrinterDriver, DocumentPrinterDriver>("noteprt");

            MakeDriver<ILabelPrint, LabelPrintWrap, LabelPrinterDriver>("labelprt");

            MakeDriver<IRecognizer, RecognizerWrap, CameraRecognizerDriver>("recognize");

            MakeDriver<ISiderShow, DisplayWrap, SiderShowDriver>("display");

            MakeDriver<ICatalog, CatalogWrap, MemoryCatalogDriver>("catalog");

            MakeDriver<IJournal, JournalWrap, FileJournalDriver>("journal");

            Start();
        }
    }
}