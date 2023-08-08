using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using System.Windows.Controls;
using ChainFx;

namespace ChainEdge;

/// <summary>
/// An abstract device driver, for input or output, or both.
/// </summary>
public abstract class Driver : DockPanel, IKeyable<string>, IEnumerable<Job>, INotifyCollectionChanged
{
    readonly ConcurrentQueue<Job> queue;

    // job queue that is normally put by dispatcher
    readonly BlockingCollection<Job> coll;

    // job runner
    private Thread doer;

    private int period;

    // ui
    //
    ListView lstview;


    protected Driver(int period = 100)
    {
        coll = new(queue = new ConcurrentQueue<Job>());

        this.period = period;

        if (period > 0)
        {
            doer = new Thread(() =>
            {
                while (!coll.IsCompleted)
                {
                    // take output job and render
                    if (coll.TryTake(out var job, period))
                    {
                        job.Do();
                    }

                    // // check & do input 
                    // if (TryGetInput(out var ret, period))
                    // {
                    //     // Core.Queue
                    // }
                }
            });
            doer.Start();
        }
    }

    public void Add(Job v)
    {
        coll.Add(v);

        CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add));
    }


    // UI constructs


    public abstract void Test();

    public virtual bool TryGetInput(out (decimal v, JObj ext) result, int milliseconds)
    {
        result = default;
        return false;
    }

    public bool IsInstalled()
    {
        return true;
    }

    public virtual void OnInitialize()
    {
    }

    public string ClassName => GetType().Name;

    public string Key { get; set; }

    public void OnClose()
    {
    }

    public event NotifyCollectionChangedEventHandler CollectionChanged;

    IEnumerator IEnumerable.GetEnumerator()
    {
        return queue.GetEnumerator();
    }

    public IEnumerator<Job> GetEnumerator()
    {
        return queue.GetEnumerator();
    }
}