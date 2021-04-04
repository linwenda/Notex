using Funzone.BuildingBlocks.Domain;

namespace Funzone.Services.Identity.Domain.UserRoles
{
    public class UserRole : ValueObject
    {
        public static UserRole Guest => new UserRole(nameof(Guest));
        public static UserRole Subscriber => new UserRole(nameof(Subscriber));
        public static UserRole Administrator => new UserRole(nameof(Administrator));

        public string Code { get; }

        private UserRole(string code)
        {
            Code = code;
        }
    }
}