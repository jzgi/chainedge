using ChainFX;

namespace ChainEdge;

/// <summary>
/// The embedded web proxy particularly for LAN clients.
/// </summary>
public class EmbedApp : Application
{
    public static void Initialize()
    {
        const string STATIC_ROOT = "static";

        CreateService<EmbedService>("embed", STATIC_ROOT);
    }
}