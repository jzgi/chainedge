using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using System.Threading;
using ChainFx;

namespace ChainEdge;

[ClassInterface(ClassInterfaceType.AutoDual)]
[ComVisible(true)]
public class EdgeCore
{
    // incoming events (jobj or jarr)
    BlockingCollection<ISource> incomingq = new(new ConcurrentQueue<ISource>());

    // event & job dispatcher
    private readonly Thread dispatcher;

    // outgoing messages, entered through driver threads
    private ConcurrentQueue<string> outgoing = new ConcurrentQueue<string>();

    public EdgeCore()
    {
        dispatcher = new Thread(x =>
        {
            var t = incomingq.Take();
        });
    }


    public void AddEvent(JObj e)
    {
    }
}