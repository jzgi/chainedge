namespace ChainEdge.Drivers
{
    public abstract class Driver
    {
        public abstract void Test();

        public bool IsInstalled()
        {
            return true;
        }

        public void OnInitialize()
        {
        }

        public void OnClose()
        {
        }
    }
}