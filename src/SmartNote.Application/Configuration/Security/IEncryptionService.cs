using SmartNote.Application.Configuration.DependencyInjection;

namespace SmartNote.Application.Configuration.Security;

public interface IEncryptionService : ITransientLifetime
{
    string HashPassword(string password);
    bool VerifyHashedPassword(string hashedPassword, string password);
}