namespace ChainEdge.Features
{
    public interface IReceiptPrint : IFeature
    {
        void printBizlabel();
        
        void PrintRow(short idx, string name, decimal price, short qty);

    }
}