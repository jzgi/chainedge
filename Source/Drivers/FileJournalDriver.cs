using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using EdgeQ.Features;

namespace EdgeQ.Drivers
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ComVisible(true)]
    public class FileJournalDriver : Driver, IJournal
    {
        public override void Test()
        {
            throw new NotImplementedException();
        }

        public int Count { get; }

        public void add(int id, string name, decimal price)
        {
            lst.Add(new Buy {id = id, name = name, price = price});
        }

        [IndexerName("Items")]
        public Buy this[int idx]
        {
            get => lst[idx];
            set => lst[idx] = value;
        }

        private List<Buy> lst = new List<Buy>();
    }
}