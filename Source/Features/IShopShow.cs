namespace Edgely.Features
{
    /// <summary>
    /// A secondary display to the host terminal.
    /// </summary>
    public interface IShopShow : IFeature
    {
        void open(string uri);
    }
}