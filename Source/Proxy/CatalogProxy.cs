using System;

namespace SkyEdge.Proxy
{
    public class CatalogProxy : _Proxy, ICatalog
    {
        public Item this[int idx] => throw new NotImplementedException();
    }
}