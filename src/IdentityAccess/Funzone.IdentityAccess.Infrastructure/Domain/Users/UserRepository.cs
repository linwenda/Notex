using System.Threading.Tasks;
using Funzone.IdentityAccess.Domain.Users;
using Funzone.IdentityAccess.Infrastructure.DataAccess;

namespace Funzone.IdentityAccess.Infrastructure.Domain.Users
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