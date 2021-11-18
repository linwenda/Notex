using System;
using System.Threading.Tasks;
using MarchNote.Domain.Shared;
using MarchNote.Domain.Spaces;

namespace MarchNote.Application.Spaces
{
    public class SpaceChecker : ISpaceChecker
    {
        private readonly IRepository<Space> _spaceRepository;

        public SpaceChecker(IRepository<Space> spaceRepository)
        {
            _spaceRepository = spaceRepository;
        }

        public async Task<int> CalculateSpaceCountAsync(Guid userId)
        {
            return await _spaceRepository.CountAsync(s => s.AuthorId == userId);
        }

        public async Task<bool> IsUniqueNameAsync(Guid userId, string spaceName)
        {
            return !await _spaceRepository.AnyAsync(s => s.AuthorId == userId && s.Name == spaceName);
        }
    }
}