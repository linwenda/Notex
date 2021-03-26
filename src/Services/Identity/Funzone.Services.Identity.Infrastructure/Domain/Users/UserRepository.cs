using System.Threading.Tasks;
using Funzone.Services.Identity.Domain.Users;
using Funzone.Services.Identity.Infrastructure.DataAccess;

namespace Funzone.Services.Identity.Infrastructure.Domain.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly IdentityAccessContext _context;

        public UserRepository(IdentityAccessContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }
    }
}