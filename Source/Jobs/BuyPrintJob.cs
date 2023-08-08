using ChainEdge.Drivers;

namespace ChainEdge.Jobs;

public class BuyPrintJob : Job<ESCPOSSerialPrintDriver>
{
    private Buy buy;

    public BuyPrintJob(Buy buy)
    {
        this.buy = buy;
    }

    protected internal override void Do()
    {
        var drv = Driver;
        
        
    }
}