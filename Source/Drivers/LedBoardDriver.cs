using System.Windows;

namespace ChainEdge.Drivers
{
    public class LedBoardDriver : Driver
    {
        MediaPlayWindow sidewin;


        protected internal override void OnCreate(object state)
        {
        }

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