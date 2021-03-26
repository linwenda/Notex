using Funzone.BuildingBlocks.Domain;

namespace Funzone.Services.Identity.Domain.Users.Exceptions
{
    public class UserNameMustBeUniqueException : BusinessValidationException
    {
        public UserNameMustBeUniqueException() : base("User with this email already exists.")
        {
        }
    }
}