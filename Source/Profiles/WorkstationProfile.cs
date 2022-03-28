using System.Runtime.InteropServices;
using SkyGate.Features;

namespace SkyGate.Profiles
{
    /// <summary>
    /// Workstation profile
    /// </summary>
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ComVisible(true)]
    public class WorkstationProfile : Profile
    {
        public WorkstationProfile() : base("workst", "综合工作站",
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