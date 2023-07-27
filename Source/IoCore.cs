using System.Collections.Concurrent;
using ChainFx;

namespace ChainEdge;

public class IoCore
{
    
    // input queue
    BlockingCollection<JObj> inputs = new(new ConcurrentQueue<JObj>());

    // input dispatcher
    void InputDispatch()
    {
        
    }
    
    // output queue
    BlockingCollection<JObj> outputs = new(new ConcurrentQueue<JObj>());
    
    // output dispatcher
    void OutputDispatch()
    {
        
    }
    
}