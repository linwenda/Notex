namespace Notex.Core.Aggregates.Users.DomainServices;

public interface IPasswordService
{
    string HashPassword(string password);
    bool VerifyHashedPassword(string hashedPassword, string password);
}