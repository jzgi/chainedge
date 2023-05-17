using System;
using System.IO;
using ChainFx;
using ChainFx.Web;
using Microsoft.UI.Xaml;
using Microsoft.Windows.ApplicationModel.DynamicDependency;
using Application = Microsoft.UI.Xaml.Application;

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
        static MainApp app;

        static JObj cfg;

        // the present profile 
        static Profile profile;


        private Window m_window;


        static MainApp()
        {
            // // file-based logger
            // var logfile = DateTime.Now.ToString("yyyyMM") + ".log";
            // logger = new FileLogger(logfile)
            // {
            //     Level = 3
            // };
            //
        }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            m_window = new MainWindow()
            {
                Title = "ChainEdge",
            };
            m_window.Activate();
        }

        [STAThread]
        public static void Main(string[] args)
        {
            Bootstrap.Initialize(0x00010002);
            
            Console.WriteLine("Hello World!");

            Start(_ => new MainApp());

            if (args.Length > 1)
            {
                string name = args[1];
                // profile = Profile.All[name];
            }
            else // default
            {
                // profile = Profile.All.ValueAt(0);
            }

            // // load app config
            // var bytes = File.ReadAllBytes(APP_JSON);
            // var parser = new JsonParser(bytes, bytes.Length);
            // cfg = (JObj)parser.Parse();
            //

            // win.Show();
            // Release the DDLM and clean up.
            Bootstrap.Shutdown();
        }
    }
}