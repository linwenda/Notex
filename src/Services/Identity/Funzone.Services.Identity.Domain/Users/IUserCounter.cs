namespace Funzone.Services.Identity.Domain.Users
{
    public interface IUserCounter
    {
        int CountUsersWithEmailAddress(EmailAddress emailAddress);
    }
}