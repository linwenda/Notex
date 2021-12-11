namespace SmartNote.Core.Domain.Users;

public interface IUserChecker : IDomainService
{
    Task<bool> IsUniqueEmail(string email);
}