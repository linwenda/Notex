namespace Funzone.IdentityAccess.Domain.Users
{
    public interface IUserCounter
    {
        int CountUsersWithUserName(string userName);
    }
}