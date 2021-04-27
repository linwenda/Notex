using System.Threading.Tasks;
using Funzone.Domain.SeedWork;

namespace Funzone.Domain.PostVotes
{
    public interface IPostVoteRepository : IRepository<PostVote>
    {
        Task<PostVote> GetByIdAsync(PostVoteId id);

        Task AddAsync(PostVote postVote);
    }
}