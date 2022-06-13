using Todolists.Domain.Core.Interfaces;

namespace Todolists.Domain.Services.Specs.Domain;

public static class Common
{
    public static Spec<T> NotDeleted<T>() where T : class, IDomainEntity
        => new(x => !x.IsDeleted);
    public static Spec<T> NotDeleted<T>(Guid id) where T : class, IDomainEntity
        => new(x => !x.IsDeleted & x.Id == id);
    public static Spec<T> Deleted<T>() where T : class, IDomainEntity
        => new(x => x.IsDeleted);
    public static Spec<T> ById<T>(Guid id) where T : class, IDomainEntity
        => new(x => x.Id == id);
}