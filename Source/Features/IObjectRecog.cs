namespace ChainEdge.Features
{
    public interface IObjectRecog : IFeature
    {
        // private MediaCapture d;

        int GetItemIdByScan();

        int GetNumberByScan();
    }
}