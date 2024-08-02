using System.Windows;
using System.Windows.Controls;
using static ChainEdge.EdgeApplication;

namespace ChainEdge;

/// <summary>
/// The management console for dirvers.
/// </summary>
public class DriverTabControl : TabControl
{
    internal void LoadTabs()
    {
        var map = CurrentProfile.Drivers;


        for (int i = 0; i < map.Count; i++)
        {
            var drv = map.ValueAt(i);

            // tabs.Items.Add(drv.Label);
            var tabitem = new TabItem()
            {
                Header = new Label
                {
                    Content = drv.Label,
                    Height = 38,
                    VerticalAlignment = VerticalAlignment.Center,
                },
                IsEnabled = drv.IsBound,
                Style = FocusVisualStyle,
                Content = drv
            };

            drv.TabItem = tabitem;
            Items.Add(tabitem);
        }
    }
}