using System;
using System.Collections.Generic;
using System.Windows;

namespace SkyGate
{
    /// <summary>
    /// The entry point for the application.
    /// </summary>
    public class SkyGateApp : Application
    {
        static readonly Dictionary<Type, Driver.Driver> map = new Dictionary<Type, Driver.Driver>();

        // internal static readonly Dictionary<string, Profile> features = new Dictionary<string, WrapBase>();

        static readonly Logger logger;

        // logging level
        static int logging;


        static SkyGateApp()
        {
            // file-based logger
            logging = 3;
            var logfile = DateTime.Now.ToString("yyyyMM") + ".log";
            logger = new Logger(logfile)
            {
                Level = logging
            };
        }

        protected static void Start()
        {
            var win = new MainWindow()
            {
                Title = "SkyGate",
                WindowStyle = WindowStyle.SingleBorderWindow,
                WindowState = WindowState.Maximized
            };
            var app = new SkyGateApp()
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
            Start();
        }
    }
}