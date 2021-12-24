namespace SmartNote.Domain.Users;

public interface IUserChecker : IDomainService
{
    Task<bool> IsUniqueEmail(string email);
    bool IsCorrectPassword(User user, string inputPassword);
}