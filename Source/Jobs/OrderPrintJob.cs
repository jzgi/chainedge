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

        var ctx = EdgeApplication.Win.ForeTitle;

        if (Driver is ESCPOSSerialPrintDriver drv)
        {
            drv.INIT();

            drv.JUSTIFY(1).CHARSIZE(0x00).TT(EdgeApplication.Name + " " + ctx).LF().LF();

            string name = dat["name"];
            string addr = dat["addr"];

            drv.JUSTIFY(1).CHARSIZE(0x11).TT(string.IsNullOrEmpty(addr) ? name : name + "（" + addr + "）").LF().LF();

            drv.CHARSIZE(0x00).JUSTIFY(1).TT(dat["created"]).LF().LF();

            drv.JUSTIFY(0);

            int id = dat["id"];

            drv.TT("单号：").T(id.ToString("D9")).LF();
            drv.TT("买家：").TT(dat["uname"]).LF();
            drv.TT("电话：").TT(dat["utel"]).LF();
            drv.TT("地址：").TT(dat["uarea"]).T("-").TT(dat["uaddr"]).LF();

            drv.T("-----------------------------------------------").LF();

            var lns = (JArr)dat["lns"];

            // set HT positions
            drv.HTPOS(20, 36, 52, 60);

            drv.TT("商品").HT().TT("价格").HT().TT("数量").LF().LF();
            for (int i = 0; i < lns?.Count; i++)
            {
                var ln = (JObj)lns[i];
                drv.TT(ln["name"]).HT().T(ln["price"], true).HT().T((int)ln["qty"]).T(" ").TT(ln["unit"]).LF();
            }
            drv.T("-----------------------------------------------").LF();

            int mode = dat["mode"];
            drv.TT("模式：").TT(mode == 0 ? "自行派发" : "统一派发").HT().TT("运费：").T(dat["fee"], true).HT().TT("总计：").T(dat["topay"], true).LF();

            drv.LF().LF();
            drv.LF().LF();
            drv.LF().LF();

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