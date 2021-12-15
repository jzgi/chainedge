namespace SkyEdge
{
    public interface IRecognition : IFeature
    {
        // private MediaCapture d;

        int getItemIdByScan();

        int getNumberByScan();
    }
}