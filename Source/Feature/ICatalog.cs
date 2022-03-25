namespace SkyGate.Feature
{
    public interface ICatalog : IFeature
    {
        int GetCount { get; }
        
        Post this[int idx] { get; }
    }
}