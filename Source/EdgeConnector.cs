using System.Collections.Concurrent;
using System.Threading;
using ChainFx;
using ChainFx.Web;

namespace ChainEdge;

public class EdgeConnector : WebConnector
{
    readonly BlockingCollection<JObj> outq = new(new ConcurrentQueue<JObj>());

    Thread exchanger;


    public EdgeConnector(string baseUri, WebClientHandler handler = null) : base(baseUri, handler)
    {
        exchanger = new Thread(() =>
        {
            while (!outq.IsCompleted && outq.Count != 0)
            {
                // take

                // send


                // handle response
            }
        });
    }
}