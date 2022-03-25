namespace SkyGate.Feature
{
    /// <summary>
    /// A secondary display to the host terminal.
    /// </summary>
    public interface ISiderShow : IFeature
    {
        void open(string uri);
    }
}