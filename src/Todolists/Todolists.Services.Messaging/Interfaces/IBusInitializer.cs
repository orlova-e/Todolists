using RabbitMQ.Client.Events;
using Todolists.Services.Messaging.Configuration.Models;

namespace Todolists.Services.Messaging.Interfaces;

public interface IBusInitializer
{
    void Set(Exchange exchange, Queue queue, AsyncEventHandler<BasicDeliverEventArgs> handler = null);
}