using ChainEdge.Drivers;

namespace ChainEdge.Jobs;

public class BuyPrintJob : Job
{
    private Buy buy;

    protected internal override void OnInitialize()
    {
        buy = new Buy();
        buy.Read(Data);
    }

    protected internal override void Perform()
    {
        if (Driver is ESCPOSSerialPrintDriver drv)
        {
            drv.INIT();

            drv.TT(buy.name).LF();
            for (int i = 0; i < buy.items?.Length; i++)
            {
                var it = buy.items[i];
                drv.TT(it.name).HT().HT().T(it.qty).T(" ").TT(it.unit).HT().T(it.SubTotal).LF();
            }

            // drv.LF().LF().LF().LF();
            drv.CUT();
        }
    }
}