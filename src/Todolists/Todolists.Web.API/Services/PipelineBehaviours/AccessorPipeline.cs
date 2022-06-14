using MediatR;
using Todolists.Web.API.Services.Accessors;
using Todolists.Web.API.Services.Commands;

namespace Todolists.Web.API.Services.PipelineBehaviours;

public sealed class AccessorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<AccessorBehavior<TRequest, TResponse>> _logger;
    private readonly IServiceProvider _serviceProvider;

    public AccessorBehavior(
        ILogger<AccessorBehavior<TRequest, TResponse>> logger,
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }
    
    public async Task<TResponse> Handle(
        TRequest request,
        CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        var validator = _serviceProvider.GetService<IAccessValidator<TRequest>>();
        if (validator == null)
            return await next();

        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.IsValid)
            return await next();

        return ActivatorUtilities.CreateInstance<TResponse>(
            _serviceProvider,
            OperationStatus.Forbidden);
    }
}