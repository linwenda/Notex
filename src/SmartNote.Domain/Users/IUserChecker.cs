using SmartNote.Core.Ddd;

namespace SmartNote.Domain.Users;

public interface IUserChecker : IDomainService
{
    Task<bool> IsUniqueEmail(string email);
    bool IsCorrectPassword(string hashedPassword, string inputPassword);
}