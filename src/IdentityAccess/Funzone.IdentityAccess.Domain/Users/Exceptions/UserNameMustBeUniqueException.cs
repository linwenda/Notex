namespace Funzone.IdentityAccess.Domain.Users.Exceptions
{
    public class UserNameMustBeUniqueException : IdentityAccessDomainException
    {
        public UserNameMustBeUniqueException() : base("User with this email already exists.")
        {
        }
    }
}