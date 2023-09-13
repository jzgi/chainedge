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
    public const short
        STU_ERR = -1,
        STU_VOID = 0,
        STU_READY = 1,
        STU_WORKING = 2;

    public static readonly Map<short, string> Statuses = new()
    {
        { STU_ERR, "错误" },
        { STU_VOID, "未知" },
        { STU_READY, "就绪" },
        { STU_WORKING, "运行" },
    };


    readonly ConcurrentQueue<Job> queue;

    // job queue that is normally put by dispatcher
    readonly BlockingCollection<Job> coll;

    // job runner
    private Thread doer;

    protected readonly int period;

    protected volatile short status;

    // ui
    //
    ListView lstview;


    protected Driver(int period = 200)
    {
        coll = new(queue = new ConcurrentQueue<Job>());

        lstview = new ListView()
        {
        };
        lstview.ItemsSource = this;

        this.period = period;
    }


    public void Add<J>(JObj data, int repeats = 1) where J : Job, new()
    {
        var job = new J()
        {
            Repeat = repeats,
            Data = data,
            Driver = this
        };

        // init
        job.OnInitialize();

        // add to queue
        coll.Add(job);

        // CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add));
    }


    // UI constructs

    public abstract string Label { get; }

    public short Status => status;

    public abstract void Reset();

    public void Start()
    {
        if (period <= 0) return;


        doer = new Thread(() =>
        {
            (decimal a, decimal b) last = default;

            while (!coll.IsCompleted)
            {
                // check status
                while (status < STU_READY)
                {
                    // reset and rebind
                    Reset();

                    Thread.Sleep(period);
                }

                // check & post input 
                if (TryGetInput(out var ret, period))
                {
                    bool eq = ret == last;
                    last = ret;

                    if (!eq && ret != default) // a change triggered
                    {
                        var jo = new JObj
                        {
                            { "$", Key },
                            { "a", ret.a },
                            { "b", ret.b },
                        };
                        EdgeApp.Profile.Dispatch(this, jo);
                    }
                }

                // take output job and render
                if (coll.TryTake(out var job, period))
                {
                    job.Perform();
                }
            }
        }) { Name = Key };


        // start the doer thread
        doer.Start();
    }

    public virtual void Stop()
    {
        coll.CompleteAdding();
    }

    public virtual JObj CallToPerform(JObj jo)
    {
        return null;
    }

    public virtual bool TryGetInput(out (decimal a, decimal b) result, int milliseconds)
    {
        result = default;
        return false;
    }

    public string ClassName => GetType().Name;

    public string Key { get; set; }


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