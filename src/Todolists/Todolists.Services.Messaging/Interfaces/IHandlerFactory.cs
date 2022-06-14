using RabbitMQ.Client.Events;

namespace Todolists.Services.Messaging.Interfaces;

public interface IHandlerFactory
{
    Task HandleMessageAsync<TMessage>(object sender, BasicDeliverEventArgs eventArgs);
}