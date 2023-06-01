namespace ChainEdge.Features
{
    /// <summary>
    /// A connected display.
    /// </summary>
    public interface IDisplay : IFeature
    {
        void open(string uri);
    }
}