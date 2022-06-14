using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Todolists.Services.Messaging.Configuration.Models;
using Todolists.Services.Messaging.Interfaces;

namespace Todolists.Services.Messaging.Implementation;

public class BusInitializer : IBusInitializer
{
    private readonly IConnection _connection;

    public BusInitializer(IConnection connection)
    {
        _connection = connection;
    }
    
    public void Set(Exchange exchange, Queue queue, AsyncEventHandler<BasicDeliverEventArgs> handler = null)
    {
        IModel channel = _connection.CreateModel();

        channel.BasicQos(0, 1, false);

        channel.ExchangeDeclare(exchange: exchange.Name,
            type: exchange.Type,
            durable: exchange.IsDurable,
            autoDelete: exchange.IsAutoDelete);

        channel.QueueDeclare(queue: queue.Name,
            durable: queue.IsDurable,
            exclusive: queue.IsExclusive,
            autoDelete: queue.IsAutoDelete,
            arguments: null);

        channel.QueueBind(queue: queue.Name,
            exchange: exchange.Name,
            routingKey: queue.RoutingKey,
            arguments: null);

        if (handler != null)
        {
            AsyncEventingBasicConsumer consumer = new(channel);
            consumer.Received += handler;
            channel.BasicConsume(consumer: consumer,
                queue: queue.Name,
                autoAck: false);
        }
    }
}