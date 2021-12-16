namespace SkyEdge
{
    public abstract class _Driver
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