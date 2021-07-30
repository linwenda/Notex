namespace MarchNote.Domain.Users
{
    public interface IUserChecker
    {
        bool IsUniqueEmail(string email);
        bool IsUniqueNickName(string nickName);
    }
}