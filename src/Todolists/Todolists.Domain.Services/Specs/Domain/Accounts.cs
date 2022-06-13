using Todolists.Domain.Core.Entities;

namespace Todolists.Domain.Services.Specs.Domain;

public static class Accounts
{
    public static Spec<Account> ByEmail(string email) => new(x => x.Email == email);
    public static Spec<Account> ByName(string name) => new(x => x.User.Name == name);
    public static Spec<Account> ByEmailOrUsername(string identity) => ByEmail(identity) | ByName(identity);
}