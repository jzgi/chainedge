using System.Runtime.InteropServices;
using ChainEdge.Features;

namespace ChainEdge.Profiles
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ComVisible(true)]
    public class VehicleProfile : Profile
    {
        public VehicleProfile() : base("greenh", "种植温室",
            new[]
            {
                typeof(ISoil)
            }
        )
        {
        }
    }
}