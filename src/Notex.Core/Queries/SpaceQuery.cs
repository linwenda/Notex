using Microsoft.EntityFrameworkCore;
using Notex.Core.Aggregates;
using Notex.Core.Aggregates.Spaces.ReadModels;
using Notex.Core.Configuration;
using Notex.Core.Lifetimes;

namespace Notex.Core.Queries;

public class SpaceQuery : ISpaceQuery, IScopedLifetime
{
    private readonly ICurrentUser _currentUser;
    private readonly IReadModelRepository _readModelRepository;

    public SpaceQuery(
        ICurrentUser currentUser,
        IReadModelRepository readModelRepository)
    {
        _currentUser = currentUser;
        _readModelRepository = readModelRepository;
    }

    public async Task<SpaceDetail> GetSpaceAsync(Guid id)
    {
        return await _readModelRepository.Query<SpaceDetail>().FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<IEnumerable<SpaceDetail>> GetMySpacesAsync()
    {
        return await _readModelRepository.Query<SpaceDetail>()
            .Where(s => s.CreatorId == _currentUser.Id)
            .ToListAsync();
    }
}