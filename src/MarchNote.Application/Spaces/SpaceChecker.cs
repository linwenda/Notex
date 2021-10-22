using System.Threading.Tasks;
using MarchNote.Domain.SeedWork;
using MarchNote.Domain.Spaces;
using MarchNote.Domain.Users;

namespace MarchNote.Application.Spaces
{
    public class SpaceChecker : ISpaceChecker
    {
        private readonly IRepository<Space> _spaceRepository;

        public SpaceChecker(IRepository<Space> spaceRepository)
        {
            _spaceRepository = spaceRepository;
        }

        public async Task<int> CalculateSpaceCountAsync(UserId userId)
        {
            return await _spaceRepository.CountAsync(s => s.AuthorId == userId);
        }

        public async Task<bool> IsUniqueNameAsync(UserId userId, string spaceName)
        {
            return !await _spaceRepository.AnyAsync(s => s.AuthorId == userId && s.Name == spaceName);
        }
    }
}