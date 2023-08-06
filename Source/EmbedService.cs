using ChainFx;
using ChainFx.Web;

namespace ChainEdge;

public class EmbedService : WebService, IEventPlay
{
    #region IEventPoint

    public void Add(Event v)
    {
        throw new System.NotImplementedException();
    }

    #endregion

    public void @default(WebContext wc)
    {
        wc.GivePage(200, h => { h.ALERT("this is first page"); });
    }
}