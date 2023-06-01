using System.Collections.Concurrent;
using System.Threading;

namespace ChainEdge
{
    /// <summary>
    /// An abstract device driver, of either input or output.
    /// </summary>
    public abstract class Driver
    {
        // for output
        private BlockingCollection<Job> jobq = new(new ConcurrentQueue<Job>());

        // job runner
        private Thread runner;


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
    }
}