namespace ChainEdge.Features
{
    public interface IItemRecog : IFeature
    {
        // private MediaCapture d;

        int GetItemIdByScan();

        int GetNumberByScan();
    }
}