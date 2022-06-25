namespace CoEdge.Features
{
    public interface IJournal : IFeature
    {
        int Count { get; }

        Buy this[int idx] { get; }

        void add(int id, string name, decimal price);
    }
}