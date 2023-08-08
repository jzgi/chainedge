using ChainFx;
using ChainFx.Web;

namespace ChainEdge;

public class EmbedService : WebService, IGateway
{
    #region IEventPoint

    public void Add(JObj evt)
    {
        throw new System.NotImplementedException();
    }

    #endregion

    public void @default(WebContext wc)
    {
        wc.GivePage(200, h => { h.ALERT("this is first page"); });
    }
}