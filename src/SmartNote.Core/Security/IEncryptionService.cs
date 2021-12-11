using SmartNote.Core.DependencyInjection;

namespace SmartNote.Core.Security;

public interface IEncryptionService : ITransientLifetime
{
    string HashPassword(string password);
    bool VerifyHashedPassword(string hashedPassword, string password);
}