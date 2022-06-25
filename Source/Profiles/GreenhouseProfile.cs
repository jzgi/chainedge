using System.Runtime.InteropServices;
using CoEdge.Features;

namespace CoEdge.Profiles
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ComVisible(true)]
    public class GreenhouseProfile : Profile
    {
        public GreenhouseProfile() : base("greenh", "种植温室",
            new[]
            {
                typeof(ISoil)
            }
        )
        {
        }
    }
}