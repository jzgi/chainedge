using ChainEdge.Drivers;
using ChainFX;

namespace ChainEdge.Jobs;

public class OrderPrintJob : Job
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

            drv.CHARSIZE(0x01).JUSTIFY(1).TT(EdgeApplication.Name).LF().T(" - ").TT(EdgeApplication.Tip).LF().LF();
            drv.CHARSIZE(0x11).JUSTIFY(1).TT(dat["name"]);
            drv.CHARSIZE(0x00).JUSTIFY(1).TT(dat["created"]).JUSTIFY();
            drv.LF().LF();
            drv.JUSTIFY(0);
            drv.TT(dat["uname"]).T(" ").TT(dat["utel"]).LF();
            drv.TT(dat["uaddr"]).LF();

            drv.T("-----------------------------------------------").LF();

            var lns = (JArr)dat["lns"];

            for (int i = 0; i < lns?.Count; i++)
            {
                var ln = (JObj)lns[i];
                drv.TT(ln["name"]).HT().HT().HT().TT(ln["price"]).HT().TT(ln["qty"]).T(' ').TT(ln["unit"]).LF();
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