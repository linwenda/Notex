using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Funzone.Application.Configuration.Exceptions;
using Funzone.Domain.PostVotes;
using Funzone.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Funzone.Infrastructure.DataAccess.Repositories
{
    public class PostVoteRepository : IPostVoteRepository
    {
        private readonly FunzoneDbContext _context;

        public PostVoteRepository(FunzoneDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<PostVote> GetByIdAsync(PostVoteId id)
        {
            var postVote = await _context.PostVotes.FirstOrDefaultAsync(v => v.Id == id);
            return postVote ?? throw new NotFoundException(nameof(PostVote), id);
        }

        public async Task AddAsync(PostVote postVote)
        {
            await _context.PostVotes.AddAsync(postVote);
        }
    }
}