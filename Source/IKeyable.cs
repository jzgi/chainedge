namespace CoEdge
{
    /// <summary>
    /// An object with an key name so that can be a map element.
    /// </summary>
    public interface IKeyable<out K>
    {
        K Key { get; }
    }

    public interface IGroupKeyable<K> : IKeyable<K>
    {
        /// <summary>
        /// To determine if it is in the same group as the given object. 
        /// </summary>
        /// <param name="akey"></param>
        /// <returns></returns>
        bool GroupWith(K akey);
    }

    public interface IDualKeyable<K, S> : IKeyable<K>
    {
        (K, S) CompositeKey { get; }
    }
}