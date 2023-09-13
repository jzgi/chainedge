using System;
using System.Windows;
using ChainFx;
using ChainFx.Web;
using Microsoft.Extensions.Logging;
using NAudio.Midi;
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
    internal static JObj AppConf => EmbedApp.AppConf;

    // use the embedded configure
    internal static string Name => EmbedApp.Name;


    internal static readonly EdgeWindow Win = new()
    {
        WindowStyle = WindowStyle.SingleBorderWindow,
        WindowState = WindowState.Maximized,
    };

    internal static readonly EdgeDriverWindow DriverWin = new()
    {
        WindowStyle = WindowStyle.None,
        WindowState = WindowState.Normal,
    };

    // connector to the cloud
    internal static readonly EdgeConnect Connect;

    internal static readonly Profile Profile;


    // the main application instance
    static readonly EdgeApp App = new()
    {
        MainWindow = Win,
        ShutdownMode = ShutdownMode.OnMainWindowClose,
    };

    public static readonly EdgeWrap Wrap = new();


    static EdgeApp()
    {
        EmbedApp.Initialize();

        // create client
        string url = AppConf[nameof(url)];
        if (url == null)
        {
            Logger.LogError("Wrong url in app.json");
            return;
        }
        Connect = new(url);

        // resolve current profile
        string profile = AppConf[nameof(profile)];
        Profile = Profile.GetProfile(profile);
        if (Profile == null)
        {
            Logger.LogError("Wrong profile in app.json");
            return;
        }

        DriverWin.AddChildren();
    }

    [STAThread]
    public static void Main(string[] args)
    {
        MidiOut midiOut = new MidiOut(0);
        midiOut.Send(MidiMessage.StartNote(80, 112, 2).RawData);
        midiOut.Send(MidiMessage.StopNote(60, 127, 1).RawData);

        TaskbarIconUtility.Do();

        // start the embedded web server
        EmbedApp.StartAsync(waiton: false);


        // var jo = new JObj()
        // {
        //     { "name", "子安路店" },
        //     {
        //         "items", new JArr()
        //         {
        //             new JObj()
        //             {
        //                 { "itemid", 7 },
        //                 { "name", "无铅酱油" },
        //                 { "unit", "瓶" },
        //                 { "price", 12.34M },
        //                 { "qty", 3 },
        //             },
        //             new JObj()
        //             {
        //                 { "itemid", 8 },
        //                 { "name", "大米" },
        //                 { "unit", "袋" },
        //                 { "price", 2.50M },
        //                 { "qty", 1 },
        //             },
        //         }
        //     }
        // };
        //
        // var drv = Profile.GetDriver<ESCPOSSerialPrintDriver>(null);
        // drv.Add<BuyPrintJob>(jo);

        // initial test for each & every driver
        Profile.Start();

        // win.Show();
        App.Run(Win);
        //
        // // ReSharper disable once AccessToStaticMemberViaDerivedType
        // EmbedApp.StopAsync().RunSynchronously();
    }
}