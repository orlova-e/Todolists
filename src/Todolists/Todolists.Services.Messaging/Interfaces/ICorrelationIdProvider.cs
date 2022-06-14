namespace Todolists.Services.Messaging.Interfaces;

public interface ICorrelationIdProvider
{
    Guid GetCorrelationId();
}