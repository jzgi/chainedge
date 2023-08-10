using System;
using ChainFx;
using ChainFx.Web;

namespace ChainEdge;

public class EmbedService : WebService, IGateway
{
    #region IGateway

    public void Submit(JObj v)
    {
        throw new NotImplementedException();
    }

    #endregion

    public void @default(WebContext wc)
    {
        wc.GivePage(200, h => { h.ALERT("this is first page"); });
    }
}