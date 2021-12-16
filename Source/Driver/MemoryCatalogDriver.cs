using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SkyEdge.Driver
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ComVisible(true)]
    public class MemoryCatalogDriver : _Driver, ICatalog
    {
        public override void Test()
        {
            throw new System.NotImplementedException();
        }

        public int Count { get; }

        public void add(int id, string name, decimal price)
        {
        }

        [IndexerName("Items")]
        public Item this[int idx]
        {
            get => dict[idx];
            set => dict[idx] = value;
        }

        private Dictionary<int, Item> dict = new Dictionary<int, Item>();
    }
}