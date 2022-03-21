namespace Notex.Core.Aggregates.Users.DomainServices;

public interface IUserChecker
{
    bool IsUniqueEmail(string email);
}