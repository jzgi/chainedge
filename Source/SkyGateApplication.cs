using System;
using System.Windows;
using SkyGate.Profiles;

namespace SkyGate
{
    /// <summary>
    /// The encapsulation of the application.
    /// </summary>
    public class SkyGateApplication : Application
    {
        // the global logger
        static readonly FileLogger logger;

        // the singleton application instance
        static readonly SkyGateApplication app;

        // the present profile 
        static Profile profile;


        static SkyGateApplication()
        {
            // file-based logger
            var logfile = DateTime.Now.ToString("yyyyMM") + ".log";
            logger = new FileLogger(logfile)
            {
                Level = 3
            };

            // app instance
            app = new SkyGateApplication()
            {
                MainWindow = new MainWindow()
                {
                    Title = "SkyGate",
                    WindowStyle = WindowStyle.SingleBorderWindow,
                    WindowState = WindowState.Maximized
                },
                ShutdownMode = ShutdownMode.OnMainWindowClose,
            };
        }

        [STAThread]
        public static void Main()
        {
            // win.Show();
            app.Run(app.MainWindow);

            // load cfg
            JObj jo = null;
        }
    }
}