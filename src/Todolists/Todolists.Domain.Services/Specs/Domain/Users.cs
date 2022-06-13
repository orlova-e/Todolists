using Todolists.Domain.Core.Entities;

namespace Todolists.Domain.Services.Specs.Domain;

public static class Users
{
    public static Spec<User> ByName(string name) => new(x => x.Name == name);
}