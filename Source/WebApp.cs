using ChainFx;

namespace ChainEdge;

public class WebApp : Application
{
    public static async void StartAsync()
    {
        const string STATIC_ROOT = "static";

        CreateService<WwwService>("www", STATIC_ROOT);

        await StartAsync(false);
    }
}