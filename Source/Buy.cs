using System.Runtime.InteropServices;

namespace CoEdge
{
    [ComVisible(true)]
    public class Buy
    {
        public int id { get; set; }

        public string name { get; set; }

        public decimal price { get; set; }
    }
}