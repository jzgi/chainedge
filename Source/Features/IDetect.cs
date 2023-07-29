namespace ChainEdge.Features
{
    public interface IDetect : IFeature
    {
        int GetItemIdByScan();

        int GetNumberByScan();
    }
}