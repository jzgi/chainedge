using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SkyGate.Feature;

namespace SkyGate.Driver
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ComVisible(true)]
    public class MemoryCatalogDriver : Driver, ICatalog
    {
        public override void Test()
        {
            throw new System.NotImplementedException();
        }

        public int GetCount { get; }

        public void add(int id, string name, decimal price)
        {
        }

        [IndexerName("Items")]
        public Post this[int idx]
        {
            get => dict[idx];
            set => dict[idx] = value;
        }

        private Dictionary<int, Post> dict = new Dictionary<int, Post>();
    }
}