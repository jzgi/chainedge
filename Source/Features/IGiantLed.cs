namespace DoEdge.Features
{
    /// <summary>
    /// A secondary display to the host terminal.
    /// </summary>
    public interface IGiantLed : IFeature
    {
        void open(string uri);
    }
}