using Todolists.Services.Shared.Interfaces;

namespace Todolists.Services.Shared.Implementation;

internal class EncryptionService : IEncryptionService
{
    public string ComputeHash(string value)
        => BCrypt.Net.BCrypt.HashPassword(value);

    public bool Validate(string hash, string password)
        => BCrypt.Net.BCrypt.Verify(password, hash);
}