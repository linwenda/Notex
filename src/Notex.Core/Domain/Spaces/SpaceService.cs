using Notex.Core.Domain.SeedWork;
using Notex.Core.Domain.Spaces.Exceptions;
using Notex.Core.Domain.Spaces.ReadModels;
using Notex.Messages.Shared;
using Notex.Messages.Spaces;

namespace Notex.Core.Domain.Spaces;

public class SpaceService : ISpaceService
{
    private readonly IReadOnlyRepository<SpaceDetail> _spaceRepository;

    public SpaceService(IReadOnlyRepository<SpaceDetail> spaceRepository)
    {
        _spaceRepository = spaceRepository;
    }

    public async Task<Space> CreateSpaceAsync(Guid userId, string name, string cover, Visibility visibility)
    {
        await CheckIsUniqueNameInUserSpaceAsync(userId, name);

        return Space.Initialize(userId, name, cover, visibility);
    }

    public async Task UpdateSpaceAsync(Space space, string name, string backgroundImage, Visibility visibility)
    {
        if (space.GetName() != name)
        {
            await CheckIsUniqueNameInUserSpaceAsync(space.GetCreatorId(), name);
        }

        space.Update(name, backgroundImage, visibility);
    }

    private async Task CheckIsUniqueNameInUserSpaceAsync(Guid userId, string name)
    {
        var isUniqueName = await _spaceRepository.CountAsync(s => s.CreatorId == userId && s.Name == name) == 0;

        if (!isUniqueName)
        {
            throw new SpaceNameAlreadyExistsException();
        }
    }
}