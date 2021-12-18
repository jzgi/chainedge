namespace SkyEdge
{
    public interface ICatalog : IFeature
    {
        int GetCount { get; }
        
        Item this[int idx] { get; }
    }
}