namespace Todolists.Services.Messaging.Configuration.Models;

public class Exchange
{
    public string Type { get; init; }
    public string Name { get; init; }
    public bool IsDurable { get; init; }
    public bool IsAutoDelete { get; init; }
    public IEnumerable<Queue> Queues { get; init; }
}