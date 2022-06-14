using RabbitMQ.Client.Events;

namespace Todolists.Services.Messaging.Interfaces;

public interface IMessageService
{
    void Send<T>(string routingKey, T message);
    T Deserialize<T>(BasicDeliverEventArgs eventArgs);
}