using System.Collections.Generic;

namespace SkyEdge
{
    public class _Proxy : List<_Driver>
    {
        private int active;

        public int Active => active;

        public _Driver GetActive() => this[active];
    }
}