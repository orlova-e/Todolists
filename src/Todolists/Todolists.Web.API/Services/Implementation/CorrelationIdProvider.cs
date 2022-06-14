using Todolists.Services.Messaging.Interfaces;

namespace Todolists.Web.API.Services.Implementation;

public class CorrelationIdProvider : ICorrelationIdProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CorrelationIdProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    
    public Guid GetCorrelationId()
    {
        var correlationIdString = _httpContextAccessor.HttpContext.Session.GetString("CorrelationId");
        return Guid.Parse(correlationIdString);
    }
}