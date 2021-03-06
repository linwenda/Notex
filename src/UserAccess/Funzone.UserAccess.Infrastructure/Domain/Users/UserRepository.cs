using Funzone.UserAccess.Domain.Users;
using Funzone.UserAccess.Infrastructure.DataAccess;
using System.Threading.Tasks;

namespace Funzone.UserAccess.Infrastructure.Domain.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly UserAccessContext _context;

        public UserRepository(UserAccessContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }
    }
}