using SkyGate.Feature;

namespace SkyGate.Profile
{
    public class WeighProfile : Profile
    {
        public WeighProfile() : base(
            "weigh",
            "农贸智能台秤",
            new[] {typeof(IScale)}
        )
        {
        }
    }
}