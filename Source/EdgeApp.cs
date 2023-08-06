using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing.Printing;
using System.Threading;
using System.Windows;
using ChainFx;
using ChainFx.Web;
using Microsoft.Extensions.Logging;
using Application = System.Windows.Application;

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


    internal static readonly EdgeWindow Win = new()
    {
        WindowStyle = WindowStyle.SingleBorderWindow,
        WindowState = WindowState.Maximized,
    };

    static readonly EdgeDriverWindow DriverWin = new()
    {
        WindowStyle = WindowStyle.SingleBorderWindow,
        WindowState = WindowState.Normal,
    };

    // connector to the cloud
    internal static readonly EdgeConnect Client;

    internal static readonly Profile Profile;


    // the main application instance
    static readonly EdgeApp App = new()
    {
        MainWindow = Win,
        ShutdownMode = ShutdownMode.OnMainWindowClose,
    };

    public static EdgeHost host = new();


    static EdgeApp()
    {
        EmbedApp.Initialize();

        // create client
        string url = AppConf[nameof(url)];
        if (url == null)
        {
            Logger.LogError("Please define url in app.json");
            return;
        }
        Client = new(url);

        // resolve current profile
        string profile = AppConf[nameof(profile)];
        Profile = Profile.GetProfile(profile);
        if (Profile == null)
        {
            Logger.LogError("Please define profile in app.json");
            return;
        }
    }

    [STAThread]
    public static void Main(string[] args)
    {
        // start the embedded web server
        EmbedApp.StartAsync(waiton: false);

        // win.Show();
        App.Run(Win);

        // ReSharper disable once AccessToStaticMemberViaDerivedType
        EmbedApp.StopAsync().RunSynchronously();
    }
}