namespace Funzone.Services.Identity.Domain.Users
{
    public interface IUserCounter
    {
        int CountUsersWithUserName(string userName);
    }
}