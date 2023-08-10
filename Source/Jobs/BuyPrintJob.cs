using ChainEdge.Drivers;

namespace ChainEdge.Jobs;

public class BuyPrintJob : Job
{
    private Buy buy;

    protected internal override void Initialize()
    {
        buy = new Buy();
        buy.Read(Data);
    }

    protected internal override void Perform()
    {
        if (Driver is ESCPOSSerialPrintDriver drv)
        {
            for (int i = 0; i < buy.items?.Length; i++)
            {
                var it = buy.items[i];
                drv.T(it.name).HT().T(it.SubTotal);
            }
        }
    }
}