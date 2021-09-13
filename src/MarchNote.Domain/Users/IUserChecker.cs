namespace MarchNote.Domain.Users
{
    public interface IUserChecker
    {
        bool IsUniqueEmail(string email);
    }
}