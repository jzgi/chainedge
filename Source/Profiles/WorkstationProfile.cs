using System.Runtime.InteropServices;
using CoEdge.Features;

namespace CoEdge.Profiles
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