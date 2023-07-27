using System.Collections.Concurrent;
using System.Threading;
using System.Windows.Controls;
using ChainFx;

namespace ChainEdge;

/// <summary>
/// An abstract device driver, of either input or output.
/// </summary>
public abstract class Driver : StackPanel, IData
{
    // for output
    private BlockingCollection<JObj> jobq = new(new ConcurrentQueue<JObj>());

    // job runner
    private Thread runner;
        
        
    // UI constructs
        
        


    public abstract void Test();

    public bool IsInstalled()
    {
        return true;
    }

    public virtual void OnInitialize()
    {
    }


    public void OnClose()
    {
    }

    public void Read(ISource s, short msk = 255)
    {
    }

    public void Write(ISink s, short msk = 255)
    {
    }
}