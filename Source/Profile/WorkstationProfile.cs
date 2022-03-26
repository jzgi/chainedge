using System.Runtime.InteropServices;
using SkyGate.Feature;

namespace SkyGate.Profile
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
                typeof(INotePrint),
            }
        )
        {
        }
    }
}