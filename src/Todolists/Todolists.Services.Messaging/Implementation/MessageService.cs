using System.Text;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Todolists.Services.Messaging.Interfaces;

namespace Todolists.Services.Messaging.Implementation;

public class MessageService : IMessageService
{
    private readonly ICorrelationIdProvider _correlationIdProvider;
    private readonly MessageBusParameters _busParameters;
    private readonly ILogger<MessageService> _logger;
    private readonly Lazy<IModel> _channel;

    public MessageService(
        IConnection connection,
        ICorrelationIdProvider correlationIdProvider,
        MessageBusParameters busParameters,
        ILogger<MessageService> logger)
    {
        _correlationIdProvider = correlationIdProvider;
        _busParameters = busParameters;
        _logger = logger;
        _channel = new Lazy<IModel>(() => connection.CreateModel());
    }
    
    public void Send<T>(string routingKey, T message)
    {
        var correlationId = _correlationIdProvider.GetCorrelationId().ToString();
        _logger.LogInformation("Start sending message with {correlationId}", correlationId);

        try
        {
            var properties = _channel.Value.CreateBasicProperties();
            properties.CorrelationId = correlationId;
            properties.ContentEncoding = nameof(Encoding.UTF8);
            properties.Type = message.GetType().Name;
            properties.Timestamp = new AmqpTimestamp(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());

            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
            var exchange = _busParameters.GetExchange(routingKey);

            _channel.Value.BasicPublish(exchange: exchange ?? string.Empty,
                routingKey: routingKey,
                mandatory: true,
                basicProperties: properties,
                body: body);

            _logger.LogInformation("End sending a message with {correlationId}", correlationId);
        }
        catch (Exception exc)
        {
            _logger.LogError("An error when trying to send a message with correlationId {correlationId} and routingKey '{routingKey}':\n{message}\n{stackTrace}",
                correlationId, routingKey, exc.Message, exc.StackTrace);
        }
    }
    
    public T Deserialize<T>(BasicDeliverEventArgs eventArgs)
    {
        try
        {
            var body = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
            return JsonConvert.DeserializeObject<T>(body);
        }
        catch (Exception exc)
        {
            var type = typeof(T);
            _logger.LogError("Couldn't deserialize the object of type {name}:\n{message}\n{stackTrace}", type.FullName, exc.Message, exc.StackTrace);
            throw;
        }
    }
}