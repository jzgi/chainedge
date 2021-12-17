using System;

namespace SkyEdge.Wrap
{
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