using System.Windows;
using SkyGate.Feature;

namespace SkyGate.Driver
{
    public class LargeLedDriver : Driver, ILargeLed
    {
        SideWindow sidewin;

        public override void Test()
        {
        }

        public void open(string uri)
        {
            sidewin = new SideWindow()
            {
                Top = SystemParameters.VirtualScreenTop,
                Left = SystemParameters.VirtualScreenLeft,
                Height = SystemParameters.VirtualScreenHeight,
                Width = SystemParameters.VirtualScreenWidth,
            };
            sidewin.CreateWebView2("http://www.baidu.com");
        }
    }
}