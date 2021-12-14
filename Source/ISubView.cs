namespace SkyTerm
{
    /// <summary>
    /// A secondary display to the host terminal.
    /// </summary>
    public interface ISubView : IFeature
    {
        void open(string uri);
    }
}