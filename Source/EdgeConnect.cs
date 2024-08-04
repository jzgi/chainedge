using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using ChainFX;
using ChainFX.Source;
using ChainFX.Web;
using Microsoft.Extensions.Logging;

namespace ChainEdge;

public class EdgeConnect : WebConnect, IPipe
{
    public const int POLLING_INTEVAL = 24 * 1000;

    readonly ConcurrentQueue<JObj> queue;

    readonly BlockingCollection<JObj> coll;

    readonly Thread puller;


    public EdgeConnect(string baseUri) : base(baseUri)
    {
        // set up queue
        coll = new(queue = new());

        // create the poller thread
        //
        puller = new Thread(async () =>
        {
            while (!coll.IsCompleted)
            {
                Thread.Sleep(POLLING_INTEVAL);

                // ensure token
                //
                var token = EdgeApp.Win.Token;
                if (string.IsNullOrEmpty(token))
                {
                    continue;
                }

                // collect outgoing events
                //
                JsonBuilder bdr = null;
                try
                {
                    while (queue.TryDequeue(out var v))
                    {
                        if (bdr == null) // lazy init
                        {
                            bdr = new JsonBuilder(true, 32 * 1024);
                            bdr.ARR_();
                        }

                        bdr.OBJ_();
                        v.Write(bdr);
                        bdr._OBJ();
                    }
                    bdr?._ARR();
                }
                finally
                {
                    bdr?.Clear();
                }


                // send event request amd handle the response
                //
                try
                {
                    (short status, JArr ja) ret;
                    if (bdr != null)
                    {
                        ret = await PostAsync<JArr>(nameof(IExternable.@extern), bdr, token: token);
                    }
                    else
                    {
                        ret = await GetAsync<JArr>(nameof(IExternable.@extern), token: token);
                    }
                    if (ret.status == 200)
                    {
                        for (int i = 0; i < ret.ja.Count; i++)
                        {
                            JObj jo = ret.ja[i];
                            EdgeApp.CurrentProfile.Downward(this, jo);
                        }
                    }
                }
                catch (Exception e)
                {
                    EdgeApp.Logger.LogWarning(e.Message);
                }
            }
        })
        {
            Name = "Edge Connector"
        };
        puller.Start();
    }

    public void PostData(JObj v)
    {
        if (v != null)
        {
            coll.Add(v);
        }
    }

    public async Task<(short, IContent)> GetRawAsync(string uri)
    {
        var token = EdgeApp.Win.Token;
        try
        {
            var req = new HttpRequestMessage(HttpMethod.Get, uri);
            if (token != null)
            {
                req.Headers.TryAddWithoutValidation(COOKIE, "token=" + token);
            }
            var rsp = await client.SendAsync(req, HttpCompletionOption.ResponseContentRead);
            if (rsp.StatusCode == HttpStatusCode.OK)
            {
                var bytea = await rsp.Content.ReadAsByteArrayAsync();
                string ctyp = rsp.Content.Headers.GetValue(CONTENT_TYPE);

                return (200, new WebStaticContent(bytea, ctyp));
            }
        }
        catch (Exception e)
        {
            Application.Err(e.Message);
        }
        return (404, null);
    }

    public async Task<(short, IContent)> GetAndProxyAsync(string uri, WebContext wc)
    {
        var token = EdgeApp.Win.Token;
        try
        {
            var req = new HttpRequestMessage(HttpMethod.Get, uri);
            if (token != null)
            {
                req.Headers.TryAddWithoutValidation(COOKIE, "token=" + token);
            }
            var rsp = await client.SendAsync(req, HttpCompletionOption.ResponseContentRead);
            if (rsp.StatusCode == HttpStatusCode.OK)
            {
                var bytea = await rsp.Content.ReadAsByteArrayAsync();
                string ctyp = rsp.Content.Headers.GetValue(CONTENT_TYPE);

                // headers
                foreach (var h in rsp.Headers)
                {
                    wc.SetHeader(h.Key, h.ToString());
                }
                wc.Give(200, new WebStaticContent(bytea, ctyp));
            }
        }
        catch (Exception e)
        {
            Application.Err(e.Message);
        }
        return (404, null);
    }
}