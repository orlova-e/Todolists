using Todolists.Domain.Core.Entities;

namespace Todolists.Services.Shared.Interfaces;

public interface ICurrentUserService
{
    Guid UserId { get; }
    Task<User> GetAsync(CancellationToken cancellationToken);
    User Get();
}