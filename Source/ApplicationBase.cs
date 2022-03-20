using System;
using System.Collections.Generic;
using System.Windows;

namespace SkyGate
{
    /// <summary>
    /// The application implementation.
    /// </summary>
    public class ApplicationBase : Application
    {
        static readonly Dictionary<Type, DriverBase> map = new Dictionary<Type, DriverBase>();

        internal static readonly Dictionary<string, WrapBase> features = new Dictionary<string, WrapBase>();

        static readonly ApplicationLogger logger;

        // logging level
        static int logging;


        static ApplicationBase()
        {
            // file-based logger
            logging = 3;
            var logfile = DateTime.Now.ToString("yyyyMM") + ".log";
            logger = new ApplicationLogger(logfile)
            {
                Level = logging
            };
        }

        public static void MakeDriver<F, W, D>(string name) where F : IFeature where W : WrapBase, F, new() where D : DriverBase, F, new()
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

        public static void MakeDriver<F, W, D1, D2>(string name) where F : IFeature where W : WrapBase, F, new() where D1 : DriverBase, F, new() where D2 : DriverBase, F, new()
        {
            MakeDriver<F, W, D1>(name);
            MakeDriver<F, W, D2>(name);
        }

        public static void MakeDriver<F, W, D1, D2, D3>(string name) where F : IFeature where W : WrapBase, F, new() where D1 : DriverBase, F, new() where D2 : DriverBase, F, new() where D3 : DriverBase, F, new()
        {
            MakeDriver<F, W, D1>(name);
            MakeDriver<F, W, D2>(name);
            MakeDriver<F, W, D3>(name);
        }

        public static void MakeDriver<F, W, D1, D2, D3, D4>(string name) where F : IFeature where W : WrapBase, F, new() where D1 : DriverBase, F, new() where D2 : DriverBase, F, new() where D3 : DriverBase, F, new() where D4 : DriverBase, F, new()
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
    }
}