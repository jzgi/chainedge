using System;

namespace SkyEdge.Wrap
{
    public class CatalogWrap : WrapBase<ICatalog>, ICatalog 
    {
        public int Count { get; }
        
        
        public Item this[int idx] => throw new NotImplementedException();
    }
}