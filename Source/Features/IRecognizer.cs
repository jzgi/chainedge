namespace CoEdge.Features
{
    public interface IRecognizer : IFeature
    {
        // private MediaCapture d;

        int GetItemIdByScan();

        int GetNumberByScan();
    }
}