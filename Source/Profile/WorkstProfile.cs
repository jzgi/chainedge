using SkyGate.Feature;

namespace SkyGate.Profile
{
    /// <summary>
    /// Workstation profile
    /// </summary>
    public class WorkstProfile : Profile
    {
        public WorkstProfile() : base
        (
            "workst",
            "综合工作站",
            new[] {typeof(ILargeLed)}
        )
        {
        }
    }
}