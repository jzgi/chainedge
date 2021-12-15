namespace SkyEdge
{
    public interface IHistory : IFeature
    {
        int Count { get; }

        Opn this[int idx] { get; }
    }
}