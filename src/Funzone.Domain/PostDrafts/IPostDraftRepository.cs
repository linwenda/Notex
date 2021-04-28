using System.Threading.Tasks;
using Funzone.Domain.SeedWork;

namespace Funzone.Domain.PostDrafts
{
    public interface IPostDraftRepository : IRepository<PostDraft>
    {
        Task<PostDraft> GetByIdAsync(PostDraftId id);
        Task AddAsync(PostDraft postDraft);
        void Delete(PostDraft postDraft);
    }
}