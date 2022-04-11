using System.Windows;
using System.Windows.Controls;
using Edgely.Features;

namespace Edgely.Drivers
{
    public class GiantLedDriver : Driver, IGiantLed
    {
        MediaPlayWindow sidewin;


        public override void Test()
        {
        }

        public void open(string uri)
        {
            sidewin = new MediaPlayWindow()
            {
                Top = SystemParameters.VirtualScreenTop,
                Left = SystemParameters.VirtualScreenLeft,
                Height = SystemParameters.VirtualScreenHeight,
                Width = SystemParameters.VirtualScreenWidth,
            };
        }
    }


    public class MediaPlayWindow : Window
    {
        MediaElement webvw;
    }
}