using Funzone.BuildingBlocks.Domain;
using Funzone.Services.Identity.Domain.Users;

namespace Funzone.Services.Identity.Domain.UserRoles
{
    public class UserRole : ValueObject
    {
        public UserId UserId { get; }
        public Role Role { get; }

        //Only for EF
        private UserRole()
        {

        }

        public UserRole(UserId userId, Role role)
        {
            UserId = userId;
            Role = role;
        }
    }
}