using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using System.Threading;
using ChainEdge.Jobs;

namespace ChainEdge;

[ClassInterface(ClassInterfaceType.AutoDual)]
[ComVisible(true)]
public class EdgeQueue
{
    // incoming events
    BlockingCollection<Edgie> eventq = new(new ConcurrentQueue<Edgie>());

    // event & job dispatcher
    private readonly Thread dispatcher;


    // outgoing messages, entered through driver threads
    private ConcurrentQueue<string> outgoing = new ConcurrentQueue<string>();

    public EdgeQueue()
    {
        // call handler
        var ctx = new EdgeContext()
        {
            // set context
        };

        dispatcher = new Thread(x =>
        {
            var t = eventq.Take();


            Job eh = new PrintBuyComboJob();
            eh.Perform(ctx);
        });
    }


    public void AddEvent(Edgie e)
    {
    }
}