﻿using System;
using System.Windows;
using ChainEdge.Drivers;
using ChainEdge.Jobs;
using ChainFX;
using ChainFX.Web;
using Microsoft.Extensions.Logging;

#pragma warning disable CS4014

// ReSharper disable AccessToStaticMemberViaDerivedType

namespace ChainEdge;

/// <summary>
/// The main WPF application that hosts all relevant resources.
/// </summary>
public class EdgeApplication
{
    // use the embedded logger
    internal static FileLogger Logger => EdgeWebProxy.Logger;

    // use the embedded configure
    internal static JObj AppConf => EdgeWebProxy.Config;

    // use the embedded configure
    internal static string Name => EdgeWebProxy.Nodal.name;


    internal static readonly EdgeWindow Win = new()
    {
        // WindowStyle = WindowStyle.SingleBorderWindow,
        // WindowState = WindowState.Maximized,
    };

    // connector to the cloud
    internal static readonly EdgeConnector Connector;

    internal static readonly Profile Profile;


#if !_WINDOWS
    static System.Windows.Application KitApp;
#else
    static Gtk.Application KitApp;
#endif

    // // the main application instance
    // static readonly EdgeApp App = new()
    // {
    //     MainWindow = Win,
    //     ShutdownMode = ShutdownMode.OnMainWindowClose,
    // };

    public static readonly EdgeWrap Wrap = new();


    static EdgeApplication()
    {
        // create client
        //
        string url = AppConf[nameof(url)];
        if (url == null)
        {
            Logger.LogError("missing 'url' in application.json");
            return;
        }
        Connector = new(url);

        // resolve current profile
        //
        string profile = AppConf[nameof(profile)];
        Profile = Profile.GetProfile(profile);
        if (Profile == null)
        {
            Logger.LogError("unsupported profile in application.json");
            return;
        }
    }

    [STAThread]
    public static void Main(string[] args)
    {
        // start the embedded web server
        //
        if (Profile is IProxiable)
        {
            EdgeWebProxy.Initialize();

            EdgeWebProxy.StartAsync(waiton: false);
        }


        var jo = new JObj
        {
            { "1", "万载百合广场" },
            { "created", "2024-04-02" },
            { "orgname", "中惠体验中心" },
            { "uname", "刘青云" },
            { "uaddr", "北京西城区玉渊潭102号" },
            {
                "@", new JArr()
                {
                    new JObj()
                    {
                        { "1", "无铅酱油" },
                        { "2", "1瓶" },
                        { "3", "￥23.0" },
                    },
                    new JObj()
                    {
                        { "1", "百合粉" },
                        { "2", "1包" },
                        { "3", "￥80.0" },
                    },
                }
            }
        };

        var drv = Profile.GetDriver<ESCPOSSerialPrintDriver>(null);
        drv.Add<NewOrderPrintJob>(jo);

        // initial test for each & every driver
        Profile.Start();

#if !_WINDOWS
        TaskbarIconUtility.Do();

        KitApp = new System.Windows.Application
        {
            MainWindow = Win,
            ShutdownMode = ShutdownMode.OnMainWindowClose,
        };
        // win.Show();
        KitApp.Run(Win);

#else
        Gtk.Application.Init();


        KitApp = new Gtk.Application(nameof(EdgeApplication), GLib.ApplicationFlags.None);
        KitApp.Register(GLib.Cancellable.Current);

        Gtk.Application.Run();

#endif


        //
        // // ReSharper disable once AccessToStaticMemberViaDerivedType
        // EmbedApp.StopAsync().RunSynchronously();
    }
}