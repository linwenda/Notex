using System.Threading.Tasks;

namespace Funzone.Services.Identity.Domain.Users
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
    }
}