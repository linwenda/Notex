namespace Funzone.Domain.Users
{
    public interface IUserChecker
    {
        bool IsUnique(EmailAddress emailAddress);
    }
}