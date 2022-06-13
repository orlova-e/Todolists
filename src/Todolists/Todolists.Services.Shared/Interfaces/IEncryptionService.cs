namespace Todolists.Services.Shared.Interfaces;

public interface IEncryptionService
{
    string ComputeHash(string value);
    bool Validate(string hash, string password);
}