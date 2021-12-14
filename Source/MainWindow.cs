using System.IO.Ports;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;

namespace SkyTerm
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public class MainWindow : Window
    {
        WebView2 vw;
        
        

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

            vw = new WebView2()
            {
                Height = 200, Width = 200
            };

            grid.Children.Add(vw);


            if (vw != null && vw.CoreWebView2 == null)
            {
                var env = await CoreWebView2Environment.CreateAsync(null, "data");

                await vw.EnsureCoreWebView2Async(env);
            }
            vw.CoreWebView2.Navigate("https://www.baidu.com");
            
            
            // vw.CoreWebView2.AddHostObjectToScript();
        }
    }
}