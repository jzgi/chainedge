using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using ChainFX;

namespace ChainEdge;

/// <summary>
/// An abstract device driver, for input or output, or both.
/// </summary>
public abstract class Driver : DockPanel, IKeyable<string>
{
    public const short
        STU_ERR = -1,
        STU_VOID = 0,
        STU_BOUND = 1;

    public static readonly Map<short, string> Statuses = new()
    {
        { STU_ERR, "错误" },
        { STU_VOID, "未知" },
        { STU_BOUND, "就绪" },
    };


    readonly ConcurrentQueue<Job> queue;

    // job queue that is normally put by dispatcher
    readonly BlockingCollection<Job> coll;

    // thread that handles cycles and performs jobs
    private Thread cycler;

    protected volatile bool bound;

    // ui
    //
    readonly ListBox lstbox;

    internal TabItem TabItem { get; set; }


    // the governing profile 
    public Profile Profile { get; internal set; }

    public int Period { get; internal set; }

    protected Driver(int period = 250)
    {
        coll = new(
            queue = new ConcurrentQueue<Job>()
        );

        Children.Add(lstbox = new ListBox
        {
            HorizontalContentAlignment = HorizontalAlignment.Stretch,
        });

        Period = Math.Max(period, 100);
    }


    /// <summary>
    /// Called after an instance is created.
    /// </summary>
    /// <param name="state">state object passed for driver initialization</param>
    protected internal abstract void OnCreate(object state);


    public void AddJob<J>(JObj data) where J : Job, new()
    {
        var job = new J
        {
            Data = data,
            Driver = this
        };

        // init
        job.OnInit();

        // add to queue
        coll.Add(job);


        //
        // update UI elements
        Dispatcher.Invoke(() =>
        {
            DockPanel rowp;
            var item = new ListBoxItem()
            {
                Content = rowp = new DockPanel
                {
                    LastChildFill = true,
                    HorizontalAlignment = HorizontalAlignment.Stretch
                },
                Height = 48,
            };

            UIElement prg;
            rowp.Children.Add(prg = new ProgressBar
            {
                Value = 0,
                Width = 32,
                Height = 24,
            });
            SetDock(prg, Dock.Right);

            rowp.Children.Add(new TextBlock
            {
                Text = job.ToString(),
                HorizontalAlignment = HorizontalAlignment.Stretch
            });

            lstbox.Items.Add(item);
        });
    }


    // UI constructs

    public abstract string Label { get; }

    public virtual bool IsBound => bound;

    public abstract void Bind();

    /// <summary>
    /// Starts the driver and its handling process.
    /// </summary>
    public void Start()
    {
        cycler = new Thread(() =>
        {
            (decimal a, decimal b) last = default;

            while (!coll.IsCompleted) // in fact never completed 
            {
                // check status
                while (!IsBound)
                {
                    Thread.Sleep(Period * 4);

                    Bind();

                    Dispatcher.Invoke(() =>
                    {
                        if (TabItem != null)
                        {
                            TabItem.IsEnabled = IsBound;
                        }
                    });
                }

                // check if there is an updated input 
                if (TryGetInput(out var ret, Period))
                {
                    if (ret != default || (ret == default && last != default)) // trigger
                    {
                        var jo = new JObj
                        {
                            { "$", Key },
                            { nameof(ret.a), ret.a },
                            { nameof(ret.b), ret.b },
                        };
                        EdgeApplication.CurrentProfile.Upward(this, jo);
                    }

                    // adjust last
                    last = ret;
                }

                // a period of suspending
                Thread.Sleep(Period);

                // try to take & run a job
                if (coll.TryTake(out var job, Period))
                {
                    Dispatcher.Invoke(() =>
                    {
                        // set focus
                        lstbox.SelectedIndex = 0;
                    });

                    job.Run();

                    Dispatcher.Invoke(() =>
                    {
                        // dequeue
                        lstbox.Items.RemoveAt(0);
                    });
                }
            }
        })
        {
            Name = Key
        };

        // start the doer thread
        cycler.Start();
    }

    public virtual void Stop()
    {
        coll.CompleteAdding();
    }

    public virtual JObj CallToRun(JObj jo)
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
}