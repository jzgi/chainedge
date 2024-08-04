using System;
using System.Windows;
using ChainFX;
using ChainFX.Web;
using Microsoft.Extensions.Logging;
using Application = System.Windows.Application;

#pragma warning disable CS4014

// ReSharper disable AccessToStaticMemberViaDerivedType

namespace ChainEdge;

/// <summary>
/// The main WPF application that hosts all relevant resources.
/// </summary>
public class EdgeApp : Application
{
    // use the embedded logger
    internal static FileLogger Logger => EmbedApp.Logger;

    // use the embedded configure
    internal static JObj Config => EmbedApp.Config;

    // use the embedded configure
    public static string Name => EmbedApp.Nodal.name;

    public static string Tip => EmbedApp.Nodal.tip;

    internal static readonly EdgeWindow Win;

    // connector to the cloud
    internal static readonly EdgeConnect Connect;

    internal static readonly Profile CurrentProfile;

    public static readonly EdgeWrap Wrap = new();


    // the main application instance
    static readonly EdgeApp App;


    static EdgeApp()
    {
        Win = new()
        {
            WindowStyle = WindowStyle.SingleBorderWindow,
            WindowState = WindowState.Maximized,
        };


        App = new()
        {
            MainWindow = Win,
            ShutdownMode = ShutdownMode.OnMainWindowClose,
        };

        // create client
        //
        string url = Config[nameof(url)];
        if (url == null)
        {
            Logger.LogError("missing 'url' in application.json");
            return;
        }
        Connect = new EdgeConnect(url)
        {
        };

        // resolve current profile
        //
        string profile = Config[nameof(profile)];
        var prof = Profile.All[profile];
        if (prof == null)
        {
            Logger.LogError("unsupported profile in application.json");
            return;
        }
        CurrentProfile = prof;
    }

    [STAThread]
    public static void Main(string[] args)
    {
        // start the embedded web server
        //
        if (CurrentProfile is IProxiable)
        {
            EmbedApp.Initialize();

            EmbedApp.StartAsync(waiton: false);
        }


        // var jo = new JObj
        // {
        //     { "1", "万载百合广场" },
        //     { "created", "2024-04-02" },
        //     { "orgname", "中惠体验中心" },
        //     { "uname", "刘青云" },
        //     { "uaddr", "北京西城区玉渊潭102号" },
        //     {
        //         "@", new JArr()
        //         {
        //             new JObj()
        //             {
        //                 { "1", "无铅酱油" },
        //                 { "2", "1瓶" },
        //                 { "3", "￥23.0" },
        //             },
        //             new JObj()
        //             {
        //                 { "1", "百合粉" },
        //                 { "2", "1包" },
        //                 { "3", "￥80.0" },
        //             },
        //         }
        //     }
        // };
        //
        // var drv = Profile.GetDriver<ESCPOSSerialPrintDriver>(null);
        // drv.Add<NewOrderPrintJob>(jo);
        //


        // initial test for each & every driver
        CurrentProfile.StartAll();

        TaskbarIconUtility.Do();

        // start the app and open the main windows
        App.Run(Win);

        //
        // EmbedApp.StopAsync().RunSynchronously();
    }


    public static void Trc(string msg, Exception ex = null)
    {
        if (msg != null)
        {
            Logger.Log(LogLevel.Trace, 0, msg, ex, null);
        }
    }

    public static void Dbg(string msg, Exception ex = null)
    {
        if (msg != null)
        {
            Logger.Log(LogLevel.Debug, 0, msg, ex, null);
        }
    }

    public static void Inf(string msg, Exception ex = null)
    {
        if (msg != null)
        {
            Logger.Log(LogLevel.Information, 0, msg, ex, null);
        }
    }

    public static void War(string msg, Exception ex = null)
    {
        if (msg != null)
        {
            Logger.Log(LogLevel.Warning, 0, msg, ex, null);
        }
    }

    public static void Err(string msg, Exception ex = null)
    {
        if (msg != null)
        {
            Logger.Log(LogLevel.Error, 0, msg, ex, null);
        }
    }
}