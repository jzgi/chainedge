namespace ChainEdge.Features
{
    public interface IDetect : IFeature
    {
        // private MediaCapture d;

        int GetItemIdByScan();

        int GetNumberByScan();
    }
}