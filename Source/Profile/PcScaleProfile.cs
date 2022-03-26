using System.Runtime.InteropServices;
using SkyGate.Feature;

namespace SkyGate.Profile
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ComVisible(true)]
    public class PcScaleProfile : Profile
    {
        public PcScaleProfile() : base("pcscale", "农贸智能台秤",
            new[]
            {
                typeof(IScale)
            }
        )
        {
        }
    }
}