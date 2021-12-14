using System.IO.Ports;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;

namespace SkyTerm
{
    /// <summary>
    /// The main window for the application.
    /// </summary>
    public class MainWindow : Window
    {
        WebView2 webvw;

        SubWindow subwin;


        private Grid grid;

        public MainWindow()
        {
            grid = new Grid();

            Content = grid;

            var btn = new Button
            {
                Height = 200, Width = 200
            };
            btn.Click += button1_Click;

            grid.Children.Add(btn);
        }

        async void button1_Click(object sender, RoutedEventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();

            webvw = new WebView2()
            {
                Height = 200, Width = 200
            };

            grid.Children.Add(webvw);


            if (webvw != null && webvw.CoreWebView2 == null)
            {
                var env = await CoreWebView2Environment.CreateAsync(null, "data");

                await webvw.EnsureCoreWebView2Async(env);
            }
            webvw.CoreWebView2.Navigate("https://www.baidu.com");


            // vw.CoreWebView2.AddHostObjectToScript();
        }
    }
}