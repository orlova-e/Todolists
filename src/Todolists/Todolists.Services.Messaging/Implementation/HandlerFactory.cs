using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client.Events;
using Todolists.Services.Messaging.Interfaces;

namespace Todolists.Services.Messaging.Implementation;

public class HandlerFactory : IHandlerFactory
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IMessageService _messageService;
    private readonly ILogger<HandlerFactory> _logger;

    public HandlerFactory(IServiceScopeFactory serviceScopeFactory,
        IMessageService messageService,
        ILogger<HandlerFactory> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _messageService = messageService;
        _logger = logger;
    }

    public async Task HandleMessageAsync<TMessage>(object sender, BasicDeliverEventArgs eventArgs)
    {
        var correlationId = Guid.Empty;

        try
        {
            var channel = ((AsyncEventingBasicConsumer) sender).Model;
            var properties = eventArgs.BasicProperties;
            Guid.TryParse(properties.CorrelationId, out correlationId);
            var message = _messageService.Deserialize<TMessage>(eventArgs);

            using var serviceScope = _serviceScopeFactory.CreateScope();
            
            var handler = serviceScope.ServiceProvider.GetRequiredService<IMessageHandler<TMessage>>();
            _logger.LogTrace("Handler for {Message} was created", typeof(TMessage));
            await handler.HandleAsync(message);

            channel.BasicAck(eventArgs.DeliveryTag, false);
            _logger.LogTrace("The message has been processed");
        }
        catch (Exception exc)
        {
            _logger.LogError("Unable to process the message with correlationId '{correlationId}':\n{message}\n{stackTrace}", correlationId, exc.Message, exc.StackTrace);
        }
    }
}