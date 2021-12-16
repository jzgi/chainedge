using System;
using System.Collections.Generic;
using System.Windows;

namespace SkyEdge
{
    /// <summary>
    /// The application implementation.
    /// </summary>
    public class ApplicationExt : Application
    {
        static readonly Dictionary<Type, _Driver> map = new Dictionary<Type, _Driver>();

        internal static readonly Dictionary<string, _Proxy> features = new Dictionary<string, _Proxy>();

        static readonly ApplicationLogger logger;

        // logging level
        static int logging;


        static ApplicationExt()
        {
            // file-based logger
            logging = 3;
            var logfile = DateTime.Now.ToString("yyyyMM") + ".log";
            logger = new ApplicationLogger(logfile)
            {
                Level = logging
            };
        }

        public static void MakeDriver<I, D>(string name) where I : IFeature where D : _Driver, I, new()
        {
            var typ = typeof(D);
            if (!map.TryGetValue(typ, out var drv))
            {
                drv = new D();
                map.Add(typ, drv);
            }
            if (!features.TryGetValue(name, out var lst))
            {
                lst = new _Proxy {drv};
                features.Add(name, lst);
            }
            else
            {
                lst.Add(drv);
            }
        }

        public static void MakeDriver<I, D1, D2>(string name) where I : IFeature where D1 : _Driver, I, new() where D2 : _Driver, I, new()
        {
            MakeDriver<I, D1>(name);
            MakeDriver<I, D2>(name);
        }

        protected static void Start()
        {
            var mainwin = new MainWindow()
            {
                Title = "SkyEdge",
                WindowStyle = WindowStyle.SingleBorderWindow,
                WindowState = WindowState.Maximized
            };
            var app = new ApplicationExt()
            {
                MainWindow = mainwin,
                ShutdownMode = ShutdownMode.OnMainWindowClose,
            };
            mainwin.Show();
            app.Run();
        }
    }
}