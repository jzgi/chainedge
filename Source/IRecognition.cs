namespace SkyTerm
{
    public interface IRecognition : IFeature
    {
        // private MediaCapture d;

        int getItemIdByScan();

        int getNumberByScan();
    }
}