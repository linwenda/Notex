using SmartNote.Domain;
using SmartNote.Domain.Spaces;

namespace SmartNote.Application.Spaces
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

        public async Task<bool> IsUniqueNameAsync(Guid userId, Guid exceptSpaceId, string spaceName)
        {
            return !await _spaceRepository.AnyAsync(s =>
                s.AuthorId == userId && s.Id != exceptSpaceId && s.Name == spaceName);
        }
    }
}