using SmartNote.Core.Domain.Users;
using SmartNote.Core.Shared;

namespace SmartNote.Core.Application.DomainServices;

public class UserChecker : IUserManager
{
    public Task<bool> IsUniqueEmailAsync(string email)
    {
        throw new NotImplementedException();
    }

    public string HashPassword(string password)
    {
        return PasswordManager.HashPassword(password);
    }

    public bool VerifyHashedPassword(string hashedPassword, string password)
    {
        return PasswordManager.VerifyHashedPassword(hashedPassword, password);
    }
}