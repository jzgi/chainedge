using System;
using ChainEdge;
using ChainEdge.Jobs;
using ChainFx;

namespace ChainEdge
{
    /// <summary>
    /// </summary>
    public abstract class Job
    {
        short progress;

        private JObj data;
        
        
        public abstract void Perform(EdgeContext ctx);


        public short Key => progress;
    }

    public abstract class Job<F> : Job where F : IFeature
    {
        private F feature;
        
    }
}