using System;
using ChainEdge.Features;

namespace ChainEdge.Jobs
{
    public class PrintBuyComboJob : Job<IReceiptPrint>
    {
        public override void Perform(EdgeContext ctx)
        {
            throw new NotImplementedException();
        }
    }
}