using System.Threading.Tasks;

namespace Funzone.Domain.Posts
{
    public interface IPostRepository
    {
        Task<Post> GetByIdAsync(PostId id);

        Task AddAsync(Post post);
    }
}