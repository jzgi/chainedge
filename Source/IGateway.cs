using ChainFx;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;

namespace ChainEdge;

/// <summary>
/// Both source & target for events.
/// </summary>
public interface IGateway
{
    void Add(JObj evt);
}