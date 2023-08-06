using System.Collections.Concurrent;
using System.Threading;
using ChainFx;
using ChainFx.Web;

namespace ChainEdge;

public class EdgeConnect : WebConnect, IEventPlay
{
    readonly ConcurrentQueue<Event> queue;

    readonly BlockingCollection<Event> bcoll;

    Thread puller;


    public EdgeConnect(string baseUri, WebClientHandler handler = null) : base(baseUri, handler)
    {
        bcoll = new(queue = new());

        puller = new Thread(async () =>
        {
            while (!bcoll.IsCompleted)
            {
                // at an interval
                Thread.Sleep(1000 * 12);

                // take
                // var r = bcoll.Take();

                // send

                var token = EdgeApp.Win.Token;
                var (status, ja) = await PostAsync<JArr>("/event", null, token: token);
                if (status == 200)
                {
                    for (int i = 0; i < ja.Count; i++)
                    {
                        JObj o = ja[i];

                        EdgeApp.Profile.Downstream(this, o);
                    }
                }

                // handle response
            }
        });
        puller.Start();
    }

    public void Add(Event v)
    {
        throw new System.NotImplementedException();
    }
}