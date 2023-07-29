using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using ChainEdge.Drivers;
using ChainFx;
using ChainFx.Web;
using Application = System.Windows.Application;

namespace ChainEdge;

/// <summary>
/// The main WPF application.
/// </summary>
public class MainApp : Application
{
    const string APP_JSON = "app.json";

    // the file-based logger
    static FileLogger logger;

    // the main application instance
    static MainApp app;

    static JObj cfg;

    // the present profile 

    // single threaded
    private static readonly MainQueue edgeq = new();

    public static MainQueue MainQueue => edgeq;


    static MainApp()
    {
        // instantiate
        try
        {
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
        catch (Exception e)
        {
            Debug.WriteLine(e.Message);
        }
    }


    [STAThread]
    public static void Main(string[] args)
    {
        EmbedApp.StartAsync();

        string profile = EmbedApp.App[nameof(profile)];

        var pf = Profile.GetProfile(profile);
        
        app.MainWindow.Title = EmbedApp.Name;

        // win.Show();
        app.Run(app.MainWindow);

        // ReSharper disable once AccessToStaticMemberViaDerivedType
        // EmbedApp.StopAsync().RunSynchronously();
    }
}