using System;
using System.Threading.Tasks;
using ChainFX;
using ChainFX.Web;
using Microsoft.AspNetCore.Http;

namespace ChainEdge;

public class EmbedService : WebService, IPipe
{
    #region IGateway

    public void PostData(JObj v)
    {
        throw new NotImplementedException();
    }

    #endregion


    public override async Task ProcessRequestAsync(HttpContext context)
    {
        var wc = (WebContext)context;
        try
        {
            if (wc.IsGet)
            {
                var uri = wc.Uri.Substring(1);

                // give file content from cache or file system
                var dot = uri.LastIndexOf('.');
                if (dot != -1)
                {
                    await GiveFileAsync(uri, uri.Substring(dot), wc);

                    return;
                }

                // try from cache
                if (!Service.TryGiveFromCache(wc))
                {
                    // get remote
                    var (status, cnt) = await EdgeApp.Connect.GetRawAsync(uri);

                    wc.Give(status, cnt);

                    TryAddToCache(wc);
                }
            }
            else // POST
            {
                var uri = wc.Uri;
                await EdgeApp.Connect.GetRawAsync(uri);
            }
        }
        finally
        {
            await wc.SendAsync();
        }
    }
}