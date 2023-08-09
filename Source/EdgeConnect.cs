using System.Collections.Concurrent;
using System.Threading;
using ChainFx;
using ChainFx.Web;

namespace ChainEdge;

public class EdgeConnect : WebConnect, IGateway
{
    readonly ConcurrentQueue<JObj> queue;

    readonly BlockingCollection<JObj> coll;

    readonly Thread puller;


    public EdgeConnect(string baseUri, WebClientHandler handler = null) : base(baseUri, handler)
    {
        // set up queue
        coll = new(queue = new());

        // create thread
        puller = new Thread(async () =>
        {
            while (!coll.IsCompleted)
            {
                // at an interval
                Thread.Sleep(1000 * 12);

                // ensure token
                //
                var token = EdgeApp.Win.Token;
                if (token == null) continue;

                // collect outgoing events
                //
                JsonBuilder bdr = null;
                try
                {
                    while (queue.TryDequeue(out var v))
                    {
                        if (bdr == null) // lazy init
                        {
                            bdr = new JsonBuilder(true, 32 * 1024);
                            bdr.ARR_();
                        }

                        bdr.OBJ_();
                        v.Write(bdr);
                        bdr._OBJ();
                    }
                    bdr?._ARR();
                }
                finally
                {
                    bdr?.Clear();
                }

                // send and handle response
                //
                (short status, JArr ja) ret;
                if (bdr != null)
                {
                    ret = await PostAsync<JArr>("event", bdr, token: token);
                }
                else
                {
                    ret = await GetAsync<JArr>("event", token: token);
                }
                if (ret.status == 200)
                {
                    for (int i = 0; i < ret.ja.Count; i++)
                    {
                        JObj jo = ret.ja[i];
                        EdgeApp.CurrentProfile.Downstream(this, jo);
                    }
                }
            }
        })
        {
            Name = "Edge Connect"
        };
        puller.Start();
    }

    public void Enqueue(JObj jo)
    {
        if (jo != null)
        {
            coll.Add(jo);
        }
    }
}