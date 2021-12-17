namespace SkyEdge
{
    public abstract class WrapBase
    {
        public bool IsAvailable => true;

        public abstract object GetActiveObject();
    }

    public class WrapBase<T> : WrapBase where T : IFeature
    {
        const int MAX = 4;

        readonly T[] elements = new T[MAX];

        int count;

        int active;

        public WrapBase()
        {
            count = 0;
        }

        public int Count => count;

        public T this[int idx] => elements[idx];

        public void Add(T v)
        {
            elements[count++] = v;
        }

        public int Active => active;

        public override object GetActiveObject() => elements[active];

        public T GetActive() => elements[active];
    }
}