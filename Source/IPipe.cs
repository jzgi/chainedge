using ChainFX;

namespace ChainEdge;

/// <summary>
/// A gateway is both source & target for events.
/// </summary>
public interface IPipe
{
    /// <summary>
    /// To submit an event data into the dateway.
    /// </summary>
    /// <param name="v">the event data to submit</param>
    void PostData(JObj v);
}