using System.Windows;

namespace ChainEdge.Drivers
{
    public class LedBoardDriver : Driver
    {
        MediaPlayWindow sidewin;


        public override void Rebind()
        {
        }
        public override string Label => "微屏";

        public void show(string uri)
        {
        }
    }


    public class MediaPlayWindow : Window
    {
        // MediaElement webvw;
    }
}