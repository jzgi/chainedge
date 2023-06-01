using System.Windows;
using System.Windows.Controls;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;

namespace ChainEdge
{
    /// <summary>
    /// The main window for the application.
    /// </summary>
    public class MainWindow : Window
    {
        private TabControl tabs;
        
        WebView2 webvw;

        // SideWindow subwin;


        private Grid grid;

        public MainWindow()
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
            webvw.CoreWebView2.Navigate("file://D:/ChainEdge/Test.html");
            webvw.CoreWebView2.OpenDevToolsWindow();


            // suppress new window being opened
            webvw.CoreWebView2.NewWindowRequested += (obj, args) =>
            {
                args.NewWindow = (CoreWebView2)obj;
                args.Handled = true;
            };

            webvw.CoreWebView2.AddHostObjectToScript("queue", MainApp.EdgeQueue);
        }

        public void PostMessage(string v)
        {
            webvw.CoreWebView2.PostWebMessageAsJson(v);
        }
    }
}