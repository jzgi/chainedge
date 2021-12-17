using System;
using System.Runtime.InteropServices;

namespace SkyEdge.Wrap
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ComVisible(true)]
    public class JournalWrap : WrapBase<IJournal>, IJournal
    {
        public int Count { get; }

        public Buy this[int idx] => throw new NotImplementedException();

        public void add(int id, string name, decimal price)
        {
            throw new NotImplementedException();
        }
    }
}