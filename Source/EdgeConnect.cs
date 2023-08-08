using System.Collections.Concurrent;
using System.Threading;
using ChainFx;
using ChainFx.Web;

namespace ChainEdge;

public class EdgeConnect : WebConnect, IGateway
{
    readonly ConcurrentQueue<JObj> queue;

    readonly BlockingCollection<JObj> coll;

    Thread puller;


    public EdgeConnect(string baseUri, WebClientHandler handler = null) : base(baseUri, handler)
    {
        coll = new(queue = new());

        puller = new Thread(async () =>
        {
            while (!coll.IsCompleted)
            {
                // at an interval
                Thread.Sleep(1000 * 12);

                // take
                // var r = bcoll.Take();

                // send

                var token = EdgeApp.Win.Token;
                var (status, ja) = await GetAsync<JArr>("event", token: token);
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

    public void Add(JObj evt)
    {
        throw new System.NotImplementedException();
    }
}