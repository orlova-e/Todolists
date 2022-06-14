using Microsoft.Extensions.Options;
using Todolists.Services.Messaging.Configuration.Models;
using Todolists.Services.Messaging.Interfaces;

namespace Todolists.Web.API.Services.Implementation;

public class HostedService : IHostedService
{
    private readonly IBusInitializer _busInitializer;
    private readonly MessageBus _messageBus;

    public HostedService(
        IBusInitializer busInitializer,
        IOptions<MessageBus> options)
    {
        _busInitializer = busInitializer;
        _messageBus = options.Value;
    }
    
    public Task StartAsync(CancellationToken cancellationToken)
    {
        foreach (var exchange in _messageBus.Exchanges)
        {
            foreach (var queue in exchange.Queues)
            {
                _busInitializer.Set(exchange, queue);
            }
        }
        
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}