namespace Todolists.Services.Messaging.Configuration.Models;

public class MessageBus
{
    public IEnumerable<Exchange> Exchanges { get; init; }
}