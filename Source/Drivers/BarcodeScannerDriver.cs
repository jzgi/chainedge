namespace ChainEdge.Drivers
{
    public class BarcodeScannerDriver : Driver
    {
        MediaPlayWindow sidewin;


        protected internal override void OnCreate(object state)
        {
        }

        public override void Bind()
        {
        }

        public override string Label => "条形码";

        public void show(string uri)
        {
        }
    }
}