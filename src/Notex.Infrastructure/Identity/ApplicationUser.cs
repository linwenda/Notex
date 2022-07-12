using Microsoft.AspNetCore.Identity;

namespace Notex.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string Surname { get; set; }
        public string Avatar { get; set; }
        public string Bio { get; set; }
    }
}