namespace Todolists.Services.Messaging.Configuration.Models;

public class Queue
{
    public string Name { get; init; }
    public string RoutingKey { get; init; }
    public bool IsDurable { get; init; }
    public bool IsExclusive { get; init; }
    public bool IsAutoDelete { get; init; }
}