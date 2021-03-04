using System.Threading.Tasks;

namespace Funzone.UserAccess.Domain.Users
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
    }
}