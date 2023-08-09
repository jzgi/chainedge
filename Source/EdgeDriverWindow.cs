using System.Windows;
using System.Windows.Controls;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;

namespace ChainEdge;

/// <summary>
/// The management console for dirvers.
/// </summary>
public class EdgeDriverWindow : Window
{
    private TabControl tabs;

    private Grid grid;

    public EdgeDriverWindow()
    {
        tabs = new TabControl();
    }

    internal void AddChildren()
    {
        var map = EdgeApp.CurrentProfile.Drivers;

        for (int i = 0; i < map.Count; i++)
        {
            var drv = map.ValueAt(i);

            tabs.Items.Add(drv.Label);
        }

        grid = new Grid();

        Content = tabs;

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
    }
}