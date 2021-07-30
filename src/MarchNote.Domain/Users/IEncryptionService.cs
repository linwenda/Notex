namespace MarchNote.Domain.Users
{
    public interface IEncryptionService
    {
        string HashPassword(string password);
        bool VerifyHashedPassword(string hashedPassword, string password);
    }
}