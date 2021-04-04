using Funzone.BuildingBlocks.Domain;

namespace Funzone.Services.Identity.Domain.UserRoles
{
    public class Role : ValueObject
    {
        public static Role Guest => new Role(nameof(Guest));
        public static Role Subscriber => new Role(nameof(Subscriber));
        public static Role Administrator => new Role(nameof(Administrator));

        public string Code { get; }

        private Role()
        {

        }

        private Role(string code)
        {
            Code = code;
        }
    }
}