using System.Collections.Concurrent;
using System.Threading;
using ChainFx;
using ChainFx.Web;

namespace ChainEdge;

public class EdgeConnect : WebConnect
{
    readonly BlockingCollection<JObj> outq = new(new ConcurrentQueue<JObj>());

    Thread puller;


    public EdgeConnect(string baseUri, WebClientHandler handler = null) : base(baseUri, handler)
    {
        puller = new Thread(async () =>
        {
            while (!outq.IsCompleted && outq.Count != 0)
            {
                // at an interval
                Thread.Sleep(1000 * 12);

                // take
                var r = outq.Take();

                // send

                var (status, ja) = await PostAsync<JArr>("/event", null);
                if (status == 200)
                {
                    for (int i = 0; i < ja.Count; i++)
                    {
                        JObj o = ja[i];
                        EdgeApp.QueueAdd(o);
                    }
                }

                // handle response
            }
        });
    }
}