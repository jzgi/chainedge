using System.Runtime.InteropServices;
using ChainEdge.Features;

namespace ChainEdge.Profiles
{
    /// <summary>
    /// Workstation profile
    /// </summary>
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ComVisible(true)]
    public class WarehouseProfile : Profile
    {
        public WarehouseProfile() : base("workst", "综合工作站",
            new[]
            {
                typeof(IGiantLed),
                typeof(IBillPrint),
            }
        )
        {
        }
    }
}