using System.Windows;
using System.Windows.Controls;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;

namespace ChainEdge;

/// <summary>
/// </summary>
public class EdgeDriverWindow : Window
{
    private TabControl tabs;

    WebView2 webvw;

    // SideWindow subwin;


    private Grid grid;

    public EdgeDriverWindow()
    {
        // Icon = BitmapFrame.Create(new Uri("./logo.png", UriKind.Relative));
        grid = new Grid();

        Content = grid;

        var btn = new Button
        {
            Height = 200,
            Width = 200,
            Content = "开始",
            FontSize = 24
        };
        btn.Click += button1_Click;

        grid.Children.Add(btn);
    }

    async void button1_Click(object sender, RoutedEventArgs e)
    {
        // string[] ports = SerialPort.GetPortNames();

        webvw = new WebView2
        {
            VerticalAlignment = VerticalAlignment.Stretch,
            HorizontalAlignment = HorizontalAlignment.Stretch,
        };

        grid.Children.Add(webvw);


        if (webvw != null && webvw.CoreWebView2 == null)
        {
            var env = await CoreWebView2Environment.CreateAsync(null, "data");

            await webvw.EnsureCoreWebView2Async(env);
        }
        webvw.CoreWebView2.Navigate("http://mgt.zhnt-x.com/rtlly//");
        var settings = webvw.CoreWebView2.Settings;
        settings.AreDevToolsEnabled = false;
        settings.IsZoomControlEnabled = false;
        settings.AreDefaultContextMenusEnabled = false;


        // suppress new window being opened
        webvw.CoreWebView2.NewWindowRequested += (obj, args) =>
        {
            args.NewWindow = (CoreWebView2)obj;
            args.Handled = true;
        };

        webvw.CoreWebView2.AddHostObjectToScript("queue", EdgeApp.Wrap);
    }

    public void PostMessage(string v)
    {
        webvw.CoreWebView2.PostWebMessageAsJson(v);
    }
}