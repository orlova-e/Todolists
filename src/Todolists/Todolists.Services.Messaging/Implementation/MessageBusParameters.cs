using System.Collections.Immutable;
using Microsoft.Extensions.Options;
using Todolists.Services.Messaging.Configuration.Models;

namespace Todolists.Services.Messaging.Implementation;

public class MessageBusParameters
{
    private readonly ImmutableDictionary<string, string> _queueRoutingKeyDictionary;
    private readonly ImmutableDictionary<string, string> _routingKeyExchangeDictionary;

    public MessageBusParameters(IOptions<MessageBus> options)
    {
        _queueRoutingKeyDictionary = ImmutableDictionary<string, string>.Empty;
        foreach (var queue in options.Value.Exchanges.SelectMany(e => e.Queues))
        {
            _queueRoutingKeyDictionary = _queueRoutingKeyDictionary.Add(queue.Name, queue.RoutingKey);
        }

        _routingKeyExchangeDictionary = ImmutableDictionary<string, string>.Empty;
        foreach(var exchange in options.Value.Exchanges)
        {
            foreach(var queue in exchange.Queues)
            {
                _routingKeyExchangeDictionary = _routingKeyExchangeDictionary.Add(queue.RoutingKey, exchange.Name);
            }
        }
    }

    public string GetRoutingKey(string queue)
    {
        _queueRoutingKeyDictionary.TryGetValue(queue, out var routingKey);
        return routingKey;
    }

    public string GetExchange(string routingKey)
    {
        _routingKeyExchangeDictionary.TryGetValue(routingKey, out var exchange);
        return exchange;
    }
}