using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SkyTerm
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ComVisible(true)]
    public class ItemCollection
    {
        [IndexerName("items")]
        public Item this[int index]
        {
            get => dict[index];
            set => dict[index] = value;
        }

        private Dictionary<int, Item> dict = new Dictionary<int, Item>();
    }
}