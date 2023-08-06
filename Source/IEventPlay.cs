using ChainFx;

namespace ChainEdge;

/// <summary>
/// Both source & target for events.
/// </summary>
public interface IEventPlay
{
    void Add(Event v);
}