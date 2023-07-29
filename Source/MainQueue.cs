using System.Collections.Concurrent;
using System.Threading;
using ChainFx;

namespace ChainEdge;

public class MainQueue
{
    // incoming events (jobj or jarr)
    static readonly BlockingCollection<JObj> incoming = new(new ConcurrentQueue<JObj>());

    static readonly BlockingCollection<JObj> outgoing = new(new ConcurrentQueue<JObj>());

    static readonly Thread dispatcher;

    static MainQueue()
    {
        dispatcher = new Thread(() =>
        {
            while (true)
            {
                if (incoming.IsCompleted) goto outgo;

                // take output job and render
                if (incoming.TryTake(out var job, 100))
                {
                }

                outgo:

                if (outgoing.IsCompleted) break;

                // check & do input 
                if (outgoing.TryTake(out job, 100))
                {
                }
            }
        });
    }


    public static void In(JObj v)
    {
        incoming.Add(v);
    }

    public static void Out(JObj v)
    {
        outgoing.Add(v);
    }
}