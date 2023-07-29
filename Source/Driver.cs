using System.Collections.Concurrent;
using System.Threading;
using System.Windows.Controls;
using ChainFx;

namespace ChainEdge;

/// <summary>
/// An abstract device driver, for input or output, or both.
/// </summary>
public abstract class Driver : StackPanel, IKeyable<string>
{
    // job queue that is normally put by dispatcher
    readonly BlockingCollection<IJob> jobq = new(new ConcurrentQueue<IJob>());

    // job runner
    private Thread doer;

    private int period;

    protected Driver(int period = 100)
    {
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


    public void Add(IJob job)
    {
        jobq.Add(job);
    }

    // UI constructs


    public abstract void Test();

    public virtual bool TryGetInput(out JObj v, int timeout)
    {
        v = null;
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
}