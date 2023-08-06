using System;
using ChainFx;

namespace ChainEdge;

public abstract class Event
{
    public abstract void Do();
}

/// <summary>
/// An output job that takes event content and operates on device driver.
/// </summary>
public class Event<F> : Event where F : IFeature
{
    short progress;

    readonly F feature;

    readonly JObj data;

    readonly Action<F, JObj> handler;

    public Event(F feature, JObj data, Action<F, JObj> handler)
    {
        this.feature = feature;
        this.data = data;
        this.handler = handler;
    }

    public override void Do()
    {
        handler(feature, data);
    }

    public short Progress => progress;
}