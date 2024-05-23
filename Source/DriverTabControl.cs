using System.Windows.Controls;

namespace ChainEdge;

/// <summary>
/// The management console for dirvers.
/// </summary>
public class DriverTabControl : TabControl
{
    internal void Load()
    {
        var map = EdgeApp.Profile.Drivers;

        for (int i = 0; i < map.Count; i++)
        {
            var drv = map.ValueAt(i);

            // tabs.Items.Add(drv.Label);
            Items.Add(new TabItem()
            {
                Header = drv.Label,
                Content = drv
            });
        }
    }
}