namespace ChainEdge;

public class EmbedWebApp : ChainFx.Application
{
    public static async void Setup()
    {
        const string STATIC_ROOT = "static";

        CreateService<WwwService>("www", STATIC_ROOT);

        await StartAsync(false);
    }
}