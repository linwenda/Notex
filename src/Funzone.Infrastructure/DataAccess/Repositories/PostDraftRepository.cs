using System.Threading.Tasks;
using Funzone.Application.Configuration.Exceptions;
using Funzone.Domain.PostDrafts;
using Funzone.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Funzone.Infrastructure.DataAccess.Repositories
{
    public class PostDraftRepository:IPostDraftRepository
    {
        private readonly FunzoneDbContext _context;

        public PostDraftRepository(FunzoneDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<PostDraft> GetByIdAsync(PostDraftId id)
        {
            var draft = await _context.PostDrafts.FirstOrDefaultAsync(p => p.Id == id);

            return draft ?? throw new NotFoundException(nameof(PostDraft), id);
        }

        public async Task AddAsync(PostDraft postDraft)
        {
            await _context.PostDrafts.AddAsync(postDraft);
        }

        public void Delete(PostDraft postDraft)
        {
            _context.PostDrafts.Remove(postDraft);
        }
    }
}
