using Todolists.Domain.Core.Entities;

namespace Todolists.Domain.Services.Specs.Domain;

public static class Notes<T>
    where T : NoteBase
{
    public static Spec<T> ByUser(Guid userId) => new(x => x.UserId == userId);
}