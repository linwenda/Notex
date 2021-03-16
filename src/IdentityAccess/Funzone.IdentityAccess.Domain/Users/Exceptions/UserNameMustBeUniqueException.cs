using Funzone.BuildingBlocks.Domain;

namespace Funzone.IdentityAccess.Domain.Users.Exceptions
{
    public class UserNameMustBeUniqueException : BusinessValidationException
    {
        public UserNameMustBeUniqueException() : base("User with this email already exists.")
        {
        }
    }
}