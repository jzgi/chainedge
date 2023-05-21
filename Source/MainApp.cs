using System;
using System.IO;
using System.Windows;
using ChainFx;
using ChainFx.Web;
using Application = System.Windows.Application;

namespace ChainEdge
{
    /// <summary>
    /// The encapsulation of the application.
    /// </summary>
    public class MainApp : Application
    {
        const string APP_JSON = "app.json";


        // the global logger
        static readonly FileLogger logger;

        // the singleton application instance
        static readonly MainApp app;

        static JObj cfg;

        // the present profile 
        static Profile profile;


        static MainApp()
        {
            // file-based logger
            var logfile = DateTime.Now.ToString("yyyyMM") + ".log";
            logger = new FileLogger(logfile)
            {
                Level = 3
            };

            // app instance
            app = new MainApp()
            {
                MainWindow = new MainWindow()
                {
                    Title = "ChainEdge",
                    WindowStyle = WindowStyle.SingleBorderWindow,
                    WindowState = WindowState.Maximized
                },
                ShutdownMode = ShutdownMode.OnMainWindowClose,
            };
        }

        [STAThread]
        public static void Main(string[] args)
        {
            if (args.Length > 1)
            {
                string name = args[1];
                profile = Profile.All[name];
            }
            else // default
            {
                profile = Profile.All.ValueAt(0);
            }

            // load app config
            var bytes = File.ReadAllBytes(APP_JSON);
            var parser = new JsonParser(bytes, bytes.Length);
            cfg = (JObj)parser.Parse();


            // win.Show();
            app.Run(app.MainWindow);
        }
    }
}