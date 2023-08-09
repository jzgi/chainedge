using ChainFx;

namespace ChainEdge;

/// <summary>
/// Both source & target for events.
/// </summary>
public interface IGateway
{
    void Enqueue(JObj jo);
}