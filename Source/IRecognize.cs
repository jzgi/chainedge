namespace SkyEdge
{
    public interface IRecognize : IFeature
    {
        // private MediaCapture d;

        int getItemIdByScan();

        int getNumberByScan();
    }
}