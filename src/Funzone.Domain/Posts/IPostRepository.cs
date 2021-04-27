using System.Threading.Tasks;
using Funzone.Domain.SeedWork;

namespace Funzone.Domain.Posts
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<Post> GetByIdAsync(PostId id);
        Task AddAsync(Post post);
    }
}