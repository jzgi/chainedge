using System.Collections.Concurrent;

namespace ChainEdge;

public class Profile
{
    private int InDispatch;
    
    private int OutDispatch;

    private ConcurrentDictionary<int, Driver> devices;
}