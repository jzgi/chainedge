using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Edgely
{
    public class Test
    {
    }

    // Bridge and BridgeAnotherClass are C# classes that implement IDispatch and works with AddHostObjectToScript.
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ComVisible(true)]
    public class BridgeAnotherClass
    {
        // Sample property.
        public string Prop { get; set; } = "Example";
    }

    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ComVisible(true)]
    public class Bridge 
    {
        protected  object GetActiveObject()
        {
            throw new NotImplementedException();
        }

        public string Title { get; set; } = "This is the title";

        public string Func(string param)
        {
            return "Example: " + param;
        }

        public BridgeAnotherClass AnotherObject { get; set; } = new BridgeAnotherClass();

        // Sample indexed property.
        [IndexerName("Items")]
        public string this[int index]
        {
            get { return m_dictionary[index]; }
            set { m_dictionary[index] = value; }
        }

        private Dictionary<int, string> m_dictionary = new Dictionary<int, string>();
    }
}