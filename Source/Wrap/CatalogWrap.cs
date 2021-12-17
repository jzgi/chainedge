using System;
using System.Runtime.InteropServices;

namespace SkyEdge.Wrap
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ComVisible(true)]
    public class CatalogWrap : WrapBase<ICatalog>, ICatalog
    {
        public int Count { get; set; } = 12;

        public string Title { get; set; } = "Tested OK";


        public Item this[int idx] => throw new NotImplementedException();
    }
}