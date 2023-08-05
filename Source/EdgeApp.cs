using System;
using System.Collections.Concurrent;
using System.Drawing.Printing;
using System.Threading;
using System.Windows;
using ChainFx;
using ChainFx.Web;
using Application = System.Windows.Application;

namespace ChainEdge;

/// <summary>
/// The main WPF application that hosts all relevant resources.
/// </summary>
public class EdgeApp : Application
{
    // use the embedded logger
    public static FileLogger Logger => EmbedApp.Logger;

    // use the embedded configure
    public static JObj AppConf => EmbedApp.AppConf;


    static readonly EdgeWindow Win = new()
    {
        WindowStyle = WindowStyle.SingleBorderWindow,
        WindowState = WindowState.Maximized,
    };

    static readonly DriverTabsWindow Dlg = new()
    {
        WindowStyle = WindowStyle.SingleBorderWindow,
        WindowState = WindowState.Normal,
    };

    // the main application instance
    static readonly EdgeApp App = new()
    {
        MainWindow = Win,
        ShutdownMode = ShutdownMode.OnMainWindowClose,
    };

    public static EdgeWrap Wrap = new();

    // connector to the cloud
    static EdgeConnect Conn;


    [STAThread]
    public static void Main(string[] args)
    {
        // start the embedded web server
        EmbedApp.StartAsync();

        string url = AppConf[nameof(url)];
        Conn = new(url);

        string profile = AppConf[nameof(profile)];

        var pf = Profile.GetProfile(profile);

        // win.Show();
        App.Run(Win);

        // ReSharper disable once AccessToStaticMemberViaDerivedType
        // EmbedApp.StopAsync().RunSynchronously();
    }


    //
    // queue
    //

    // incoming events (jobj or jarr)
    static readonly BlockingCollection<JObj> inq = new(new ConcurrentQueue<JObj>());

    static readonly BlockingCollection<JObj> outq = new(new ConcurrentQueue<JObj>());

    static readonly Thread dispatcher;

    static EdgeApp()
    {
        dispatcher = new Thread(() =>
        {
            while (true)
            {
                if (inq.IsCompleted) goto outgo;

                // take output job and render
                if (inq.TryTake(out var job, 100))
                {
                }

                outgo:

                if (outq.IsCompleted) break;

                // check & do input 
                if (outq.TryTake(out job, 100))
                {
                }
            }
        });
    }


    public static void QueueAdd(JObj v)
    {
        inq.Add(v);
    }

    public static void QueueTryTake(JObj v)
    {
        outq.Add(v);
    }
}