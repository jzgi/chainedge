using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using ChainFX;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;

namespace ChainEdge;

/// <summary>
/// The main window that hosts WebView2.
/// </summary>
public class EdgeWindow
{
#if _WINDOWS
    System.Windows.Window KitWin;

    readonly WebView2 webvw;
#else
    Gtk.Window KitWin;

    readonly WebKit.WebView webvw;
#endif

    readonly DockPanel dockp;


    readonly DriverTabControl tabctl;

    public EdgeWindow()
    {
#if _WINDOWS

        KitWin.Loaded += OnLoaded;
        KitWin.Closing += OnClosing;
        KitWin.Icon = BitmapFrame.Create(new Uri("./static/favicon.ico", UriKind.Relative));
        KitWin.Content = dockp = new()
        {
            LastChildFill = true,
            HorizontalAlignment = HorizontalAlignment.Stretch,
            VerticalAlignment = VerticalAlignment.Stretch
        };

        dockp.Children.Add(tabctl = new());
        dockp.Children.Add(webvw = new());

        DockPanel.SetDock(tabctl, Dock.Right);

#else
#endif
    }

    async void OnLoaded(object sender, RoutedEventArgs e)
    {
        if (webvw != null && webvw.CoreWebView2 == null)
        {
            var env = await CoreWebView2Environment.CreateAsync(null, "data");

            await webvw.EnsureCoreWebView2Async(env);
        }
        if (webvw.CoreWebView2 == null)
        {
            MessageBox.Show("failed to obtain CoreWebView2");
            return;
        }

        var setgs = webvw.CoreWebView2.Settings;
        // settings.AreDevToolsEnabled = false;
        setgs.IsZoomControlEnabled = false;
        setgs.AreDefaultContextMenusEnabled = false;

        string url = EdgeApplication.AppConf[nameof(url)];
        webvw.CoreWebView2.Navigate(url);

        // suppress new window being opened
        webvw.CoreWebView2.NewWindowRequested += (obj, args) =>
        {
            args.NewWindow = (CoreWebView2)obj;
            args.Handled = true;
        };

        //
        // handling

        webvw.CoreWebView2.WebMessageReceived += (sender, args) => { };

        webvw.CoreWebView2.AddHostObjectToScript("wrap", EdgeApplication.Wrap);

        webvw.NavigationCompleted += OnNavigationCompleted;


        // load tabs
        tabctl.LoadTabs();
    }

    volatile string token;

    public string Token => token;

    async void OnNavigationCompleted(object target, CoreWebView2NavigationCompletedEventArgs e)
    {
        // Title = webvw.CoreWebView2.DocumentTitle;

        var mgr = webvw.CoreWebView2.CookieManager;
        var cookies = await mgr.GetCookiesAsync(null); // get all cookie
        var cookie = cookies.Find(x => x.Name == "token");

        token = cookie?.Value;
    }

    public async Task<string> GetTokenAsync()
    {
        var mgr = webvw.CoreWebView2.CookieManager;
        var cookies = await mgr.GetCookiesAsync(null); // get all cookie
        var token = cookies.First(x => x.Name == "token");
        return token?.Value;
    }

    public void PostMessage(JObj v)
    {
        var str = v.ToString();

#if _WINDOWS
        KitWin.Dispatcher.Invoke(() => webvw.CoreWebView2.PostWebMessageAsJson(str));
#endif
    }

    protected void OnClosing(object sender, CancelEventArgs e)
    {
        e.Cancel = true;
        KitWin.Hide();
    }
}