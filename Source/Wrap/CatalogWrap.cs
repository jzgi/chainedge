using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SkyEdge.Wrap
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ComVisible(true)]
    public class CatalogWrap : WrapBase, ICatalog
    {
        protected override object GetActiveObject()
        {
            throw new NotImplementedException();
        }

        public int GetCount { get; set; } = 12;

        public string DisplayName { get; set; } = "Tested OK";

        [IndexerName("Items")] public Item this[int idx] => throw new NotImplementedException();
    }
}