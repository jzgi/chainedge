namespace ChainEdge.Drivers
{
    public class BarcodeScannerDriver : Driver
    {
        MediaPlayWindow sidewin;


        public override void Rebind()
        {
        }

        public override string Label => "条形码";

        public void show(string uri)
        {
        }
    }
}