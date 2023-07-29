using System;
using ChainFx;

namespace ChainEdge;

public interface IJob
{
    void Do();
}

/// <summary>
/// An output job that takes event content and operates on device driver.
/// </summary>
public struct Job<F> : IJob where F : IFeature
{
    short progress;

    readonly F feature;

    readonly JObj data;

    readonly Action<F, JObj> doer;

    public Job(F feature, JObj data, Action<F, JObj> doer)
    {
        this.feature = feature;
        this.data = data;
        this.doer = doer;
    }

    public void Do()
    {
        doer(feature, data);
    }

    public short Progress => progress;
}