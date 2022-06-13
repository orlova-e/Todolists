using System.Security.Claims;
using Todolists.Domain.Core.Entities;
using Todolists.Domain.Services.Specs.Domain;
using Todolists.Infrastructure.DataAccess;
using Todolists.Services.Shared.Interfaces;

namespace Todolists.Web.API.Services.Implementation;

internal class CurrentUserService : ICurrentUserService
{
    private readonly Lazy<Guid> _userId;
    private readonly IHttpContextAccessor _accessor;
    private readonly IRepository _repository;
    
    public Guid UserId => _userId.Value;

    public CurrentUserService(
        IHttpContextAccessor accessor,
        IRepository repository)
    {
        _accessor = accessor;
        _repository = repository;
        
        _userId = new Lazy<Guid>(() =>
        {
            var user = accessor.HttpContext.User;
            var userId = user?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId != null && Guid.TryParse(userId, out var id))
                return id;
				
            throw new InvalidOperationException("No user claims were found");
        });
    }

    public Task<User> GetAsync(CancellationToken cancellationToken)
    {
        if (!TryGetUserId(out var id))
            return null;

        return _repository.GetAsync<User, Guid>(Common.NotDeleted<User>(id), cancellationToken);
    }

    public User Get()
    {
        if (!TryGetUserId(out var id))
            return null;

        return _repository.Get<User, Guid>(Common.NotDeleted<User>(id));
    }

    private bool TryGetUserId(out Guid id)
    {
        id = Guid.Empty;

        if (!_accessor.HttpContext.User.Identity.IsAuthenticated)
            return false;

        var userClaims = _accessor.HttpContext.User.Claims;
        var userId = userClaims?.FirstOrDefault(c=> c.Type == ClaimTypes.NameIdentifier)?.Value;
        
        if (!Guid.TryParse(userId, out var parsed))
            return false;

        id = parsed;
        return true;
    }
}