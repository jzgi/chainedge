using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SkyEdge
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ComVisible(true)]
    public class FileHistoryDriver : Driver, IHistory
    {
        public override void Test()
        {
            throw new NotImplementedException();
        }

        public int Count { get; }

        public void add(int id, string name, decimal price)
        {
        }

        [IndexerName("Items")]
        public Opn this[int idx]
        {
            get => dict[idx];
            set => dict[idx] = value;
        }

        private Dictionary<int, Opn> dict = new Dictionary<int, Opn>();
    }
}