using System;
using System.Runtime.InteropServices;

namespace SkyGate.Wrap
{
    [ComVisible(true)]
    public class CatalogWrap : WrapBase, ICatalog
    {
        protected override object GetActiveObject()
        {
            throw new NotImplementedException();
        }

        public int GetCount { get; set; } = 12;

        public string DisplayName { get; set; } = "Tested OK";

        public Post this[int idx] => new Post();
    }
}