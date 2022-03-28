namespace SkyGate.Features
{
    public interface ICatalog : IFeature
    {
        int GetCount { get; }
        
        Post this[int idx] { get; }
    }
}