namespace Todolists.Services.Messaging.Interfaces;

public interface IMessageHandler<TMessage>
{
    Task HandleAsync(TMessage message);
}