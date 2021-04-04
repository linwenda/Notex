using System.Threading.Tasks;
using Funzone.Services.Identity.Domain.Users;
using Funzone.Services.Identity.Infrastructure.DataAccess;

namespace Funzone.Services.Identity.Infrastructure.Domain.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly IdentityContext _context;

        public UserRepository(IdentityContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }
    }
}