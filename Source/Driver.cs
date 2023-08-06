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
public abstract class Driver : DockPanel, IKeyable<string>, IEventPlay, IEnumerable<Event>, INotifyCollectionChanged
{
    readonly ConcurrentQueue<Event> innerq;

    // job queue that is normally put by dispatcher
    readonly BlockingCollection<Event> jobq;

    // job runner
    private Thread doer;

    private int period;


    // ui
    //
    ListView lstview;


    protected Driver(int period = 100)
    {
        jobq = new(innerq = new ConcurrentQueue<Event>());


        this.period = period;

        if (period > 0)
        {
            doer = new Thread(() =>
            {
                while (!jobq.IsCompleted)
                {
                    // take output job and render
                    if (jobq.TryTake(out var job, period))
                    {
                        job.Do();
                    }

                    // check & do input 
                    if (TryGetInput(out var ret, period))
                    {
                        // Core.Queue
                    }
                }
            });
        }
    }

    public void Add(JObj v)
    {
        throw new System.NotImplementedException();
    }

    public void Add(Event job)
    {
        jobq.Add(job);

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
        return innerq.GetEnumerator();
    }

    public IEnumerator<Event> GetEnumerator()
    {
        return innerq.GetEnumerator();
    }
}