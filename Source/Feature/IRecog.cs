namespace SkyGate.Feature
{
    public interface IRecog : IFeature
    {
        // private MediaCapture d;

        int GetItemIdByScan();

        int GetNumberByScan();
    }
}