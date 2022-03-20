namespace SkyGate
{
    /// <summary>
    /// A secondary display to the host terminal.
    /// </summary>
    public interface IDisplay : IFeature
    {
        void open(string uri);
    }
}