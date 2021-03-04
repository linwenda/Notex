namespace Funzone.UserAccess.Domain.Users.Exceptions
{
    public class UserNameMustBeUniqueException : UserAccessDomainException
    {
        public UserNameMustBeUniqueException() : base("User with this email already exists")
        {
        }
    }
}