using ChainFx;

namespace ChainEdge;

/// <summary>
/// The embedded web application.
/// </summary>
public class EmbedApp : Application
{
    public static async void StartAsync()
    {
        const string STATIC_ROOT = "static";

        CreateService<EmbedService>("embed", STATIC_ROOT);

        await StartAsync(waiton: false);
    }
}