namespace SmartNote.Core.Domain.Users;

public interface IUserManager : IDomainService
{
    Task<bool> IsUniqueEmailAsync(string email);
    string HashPassword(string password);
    bool VerifyHashedPassword(string hashedPassword, string password);
}