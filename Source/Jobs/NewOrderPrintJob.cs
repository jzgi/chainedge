using ChainEdge.Drivers;
using ChainFX;

namespace ChainEdge.Jobs;

public class NewOrderPrintJob : Job
{
    public override void OnInitialize()
    {
    }

    public override void Perform()
    {
        var dat = Data;

        if (Driver is ESCPOSSerialPrintDriver drv)
        {
            drv.INIT();

            drv.CHARSIZE(1).JUSTIFY(1).TT(dat["1"]).JUSTIFY().CHARSIZE().LF().LF().JUSTIFY(1).TT(dat["created"]).JUSTIFY();
            drv.LF().LF();
            drv.TT(dat["orgname"]).HT().HT().TT(dat["uname"]).LF();
            drv.HT().HT().HT().TT(dat["uaddr"]).LF();

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
        if (Driver.Profile is IProxiable)
        {
            return Data["orgname"];
        }
        else
        {
            return Data["uname"];
        }
    }
}