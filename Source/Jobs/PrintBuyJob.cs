using System.Runtime.InteropServices;
using ChainEdge.Features;

namespace ChainEdge.Jobs
{
    public class PrintBuyJob : Job<IReceiptPrint>
    {
        public override void Perform(EdgeContext ctx)
        {
            throw new System.NotImplementedException();
        }
    }
}