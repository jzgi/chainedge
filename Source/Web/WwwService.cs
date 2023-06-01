using ChainFx.Web;

namespace ChainEdge;

public class WwwService : WebService
{
    public void @default(WebContext wc)
    {
        wc.GivePage(200, h =>
        {
            h.ALERT("this is first page");
        });
    }
}