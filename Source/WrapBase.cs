using System.Collections.Generic;

namespace SkyEdge
{
    public abstract class WrapBase
    {
        readonly List<IFeature> list = new List<IFeature>();

        public bool IsAvailable => true;

        internal void Add(IFeature v)
        {
            list.Add(v);
        }


        protected abstract object GetActiveObject();
    }
}