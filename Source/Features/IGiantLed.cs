namespace ChainEdge.Features
{
    /// <summary>
    /// A giant LED display.
    /// </summary>
    public interface IGiantLed : IFeature
    {
        void open(string uri);
    }
}