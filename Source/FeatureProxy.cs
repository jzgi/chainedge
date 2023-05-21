using System.Runtime.InteropServices;

namespace ChainEdge;

[ClassInterface(ClassInterfaceType.AutoDual)]
[ComVisible(true)]
public class FeatureProxy
{
    private string feature;

    public int Age { get; set; } = 2;
}