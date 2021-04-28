using System.Threading.Tasks;
using Funzone.Application.Configuration.Exceptions;
using Funzone.Domain.Posts;
using Funzone.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Funzone.Infrastructure.DataAccess.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly FunzoneDbContext _context;

        public PostRepository(FunzoneDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<Post> GetByIdAsync(PostId id)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);
            return post ?? throw new NotFoundException(nameof(Post), id);
        }

        public async Task AddAsync(Post post)
        {
            await _context.Posts.AddAsync(post);
        }
    }
}