using ChainEdge.Drivers;
using ChainFX;

namespace ChainEdge.Jobs;

public class BuyPrintJob : Job
{
    public override void OnInitialize()
    {
    }

    public override void Perform()
    {
        var dat = Data;

        if (Driver is ESCPOSSerialPrintDriver drv)
        {
            drv.Init();

            drv.CHARSIZE(1).JUSTIFY(1).TT(dat["1"]).JUSTIFY().CHARSIZE().LF().LF().JUSTIFY(1).TT(dat["2"]).JUSTIFY();
            drv.LF().LF();
            drv.TT(dat["3"]).HT().HT().TT(dat["4"]).LF();
            drv.HT().HT().HT().TT(dat["5"]).LF();

            drv.T("-----------------------------------------------").LF();

            var dtl = (JArr)dat["@"];

            for (int i = 0; i < dtl?.Count; i++)
            {
                var it = (JObj)dtl[i];
                drv.TT(it["1"]).HT().HT().HT().TT(it["2"]).T(" ").HT().TT(it["3"]).LF();
            }
            drv.T("-----------------------------------------------").LF();

            drv.LF().LF().LF().LF();

            drv.FullCut();
        }
    }

    public override string ToString()
    {
        return "Buy Print";
    }
}