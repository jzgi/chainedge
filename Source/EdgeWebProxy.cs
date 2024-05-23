using System.Drawing;
using ChainFX;

namespace ChainEdge;

/// <summary>
/// The embedded web application.
/// </summary>
public class EdgeWebProxy : Application
{
    public static void Initialize()
    {
        const string STATIC_ROOT = "static";

        CreateService<EdgeWebService>("embed", STATIC_ROOT);
    }
}