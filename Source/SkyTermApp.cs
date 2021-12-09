using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;

namespace SkyTerm
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public class SkyTermApp : Application
    {

        

        [STAThread]
        public static void Main()
        {
            var app = new SkyTermApp();
            app.Run(new MainWindow());
        }

    }
}