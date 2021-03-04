namespace Funzone.UserAccess.Domain.Users
{
    public interface IUserCounter
    {
        int CountUsersWithUserName(string userName);
    }
}