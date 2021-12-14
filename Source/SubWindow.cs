using System.Windows;
using Microsoft.Web.WebView2.Wpf;

namespace SkyTerm
{
    /// <summary>
    /// The secondary window of the main window.
    /// </summary>
    public class SubWindow : Window
    {
        // the main window that show() this window
        MainWindow mainin;

        WebView2 webvw;

        // the current source uri in the web view
        string uri;
    }
}