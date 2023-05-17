namespace ChainEdge.Features
{
    /// <summary>
    /// A organic waste recycle feature.
    /// </summary>
    public interface IRecycle : IFeature
    {
        void open(string uri);
    }
}