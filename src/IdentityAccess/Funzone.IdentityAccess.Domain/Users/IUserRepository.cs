using System.Threading.Tasks;

namespace Funzone.IdentityAccess.Domain.Users
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
    }
}