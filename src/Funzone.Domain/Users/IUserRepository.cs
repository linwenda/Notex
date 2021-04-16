using System.Threading.Tasks;
using Funzone.Domain.SeedWork;

namespace Funzone.Domain.Users
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByIdAsync(UserId userId);
        
        Task AddAsync(User user);

        void Update(User user);
    }
}