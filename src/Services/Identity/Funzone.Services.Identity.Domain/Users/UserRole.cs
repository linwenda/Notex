using Funzone.BuildingBlocks.Domain;

namespace Funzone.Services.Identity.Domain.Users
{
    public class UserRole : ValueObject
    {
        public static UserRole Member => new UserRole(nameof(Member));
        public static UserRole Administrator => new UserRole(nameof(Administrator));
        public string Code { get; private set; }

        //Only for EF
        private UserRole()
        {
        }

        private UserRole(string code)
        {
            Code = code;
        }
    }
}