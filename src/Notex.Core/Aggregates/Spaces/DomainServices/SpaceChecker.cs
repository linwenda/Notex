using Notex.Core.Aggregates.Spaces.ReadModels;
using Notex.Core.Lifetimes;

namespace Notex.Core.Aggregates.Spaces.DomainServices;

public class SpaceChecker : ISpaceChecker,IScopedLifetime
{
    private readonly IReadModelRepository _readModelRepository;

    public SpaceChecker(IReadModelRepository readModelRepository)
    {
        _readModelRepository = readModelRepository;
    }

    public bool IsUniqueNameInUserSpace(Guid userId, string name)
    {
        return !_readModelRepository.Query<SpaceDetail>().Any(s => s.CreatorId == userId && s.Name == name);
    }
}