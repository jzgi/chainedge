namespace SkyEdge.Proxy
{
    public class JournalProxy : _Proxy, IJournal
    {
        public Buy this[int idx] => throw new System.NotImplementedException();

        public void add(int id, string name, decimal price)
        {
            throw new System.NotImplementedException();
        }
    }
}