using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using ChainFX;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;

namespace ChainEdge;

/// <summary>
/// The main window that hosts WebView2.
/// </summary>
public class EdgeWindow : Window, IPipe
{
    readonly WebView2 webvw;

    readonly DockPanel dockp;

    readonly DriverTabControl tabctl;

    public EdgeWindow()
    {
        Loaded += OnLoaded;
        Closing += OnClosing;
        Icon = BitmapFrame.Create(new Uri("./static/favicon.ico", UriKind.Relative));
        Content = dockp = new()
        {
            LastChildFill = true,
            HorizontalAlignment = HorizontalAlignment.Stretch,
            VerticalAlignment = VerticalAlignment.Stretch
        };

        dockp.Children.Add(tabctl = new()
        {
            TabStripPlacement = Dock.Bottom
        });
        dockp.Children.Add(webvw = new()
        {
        });

        DockPanel.SetDock(tabctl, Dock.Right);
    }

    async void OnLoaded(object sender, RoutedEventArgs e)
    {
        if (webvw != null && webvw.CoreWebView2 == null)
        {
            var env = await CoreWebView2Environment.CreateAsync(null, "data", new CoreWebView2EnvironmentOptions()
            {
                ScrollBarStyle = CoreWebView2ScrollbarStyle.FluentOverlay
            });

            await webvw.EnsureCoreWebView2Async(env);
        }
        if (webvw.CoreWebView2 == null)
        {
            MessageBox.Show("failed to obtain CoreWebView2");
            return;
        }

        var setgs = webvw.CoreWebView2.Settings;
        setgs.AreDevToolsEnabled = true;
        setgs.IsZoomControlEnabled = false;
        setgs.AreDefaultContextMenusEnabled = false;
        setgs.IsWebMessageEnabled = true;

        string url = EdgeApp.Config[nameof(url)];
        webvw.CoreWebView2.Navigate(url);

        // suppress new window being opened
        webvw.CoreWebView2.NewWindowRequested += (obj, args) =>
        {
            args.NewWindow = (CoreWebView2)obj;
            args.Handled = true;
        };

        webvw.CoreWebView2.NavigationCompleted += OnNavigationCompleted;
        //
        // handling

        webvw.CoreWebView2.WebMessageReceived += (sender, args) =>
        {
            var str = args.TryGetWebMessageAsString();
            if (str == null) return;

            try
            {
                // jobj or jarr
                var jo = (JObj)new JsonParser(str).Parse();
                EdgeApp.CurrentProfile.Downward(this, jo);
            }
            catch (Exception e)
            {
            }
        };

        webvw.CoreWebView2.AddHostObjectToScript("wrap", EdgeApp.Wrap);

        tabctl.BorderThickness = new Thickness(0);
        // load tabs
        tabctl.LoadTabs();
    }


    volatile string token;

    public string ForeTitle { get; internal set; }

    public string Token => token;

    async void OnNavigationCompleted(object target, CoreWebView2NavigationCompletedEventArgs e)
    {
        // set windows title
        Title = webvw.CoreWebView2.DocumentTitle;

        ForeTitle = Title.Split('-')[0];

        // get access token to the platform services
        var mgr = webvw.CoreWebView2.CookieManager;
        var cookies = await mgr.GetCookiesAsync(null); // get all cookie
        var cookie = cookies.Find(x => x.Name == "token");

        token = cookie?.Value;
    }

    public void PostData(JObj v)
    {
        var str = v.ToString();

        Dispatcher.Invoke(() => webvw.CoreWebView2.PostWebMessageAsJson(str));
    }

    public async Task<string> GetTokenAsync()
    {
        var mgr = webvw.CoreWebView2.CookieManager;
        var cookies = await mgr.GetCookiesAsync(null); // get all cookie
        var token = cookies.First(x => x.Name == "token");
        return token?.Value;
    }

    protected void OnClosing(object sender, CancelEventArgs e)
    {
        e.Cancel = true;

        Hide();
    }
}