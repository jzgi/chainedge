namespace SkyTerm
{
    public interface IRecognit : IFeature
    {
        // private MediaCapture d;

        int getItemIdByScan();

        int getNumberByScan();
    }
}