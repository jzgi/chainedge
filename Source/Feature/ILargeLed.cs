namespace SkyGate.Feature
{
    /// <summary>
    /// A secondary display to the host terminal.
    /// </summary>
    public interface ILargeLed : IFeature
    {
        void open(string uri);
    }
}