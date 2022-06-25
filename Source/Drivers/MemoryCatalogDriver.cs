using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using CoEdge.Features;

namespace CoEdge.Drivers
{
    public class MemoryCatalogDriver : Driver, ICatalog
    {
        public override void Test()
        {
            throw new NotImplementedException();
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