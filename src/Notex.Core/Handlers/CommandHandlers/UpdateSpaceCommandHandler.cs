using MediatR;
using Notex.Core.Domain.SeedWork;
using Notex.Core.Domain.Spaces;
using Notex.Core.Identity;
using Notex.Messages.Spaces.Commands;

namespace Notex.Core.Handlers.CommandHandlers;

public class UpdateSpaceCommandHandler : ICommandHandler<UpdateSpaceCommand>
{
    private readonly ISpaceService _spaceService;
    private readonly IResourceAuthorizationService _resourceAuthorizationService;
    private readonly IEventSourcedRepository<Space> _spaceRepository;

    public UpdateSpaceCommandHandler(
        ISpaceService spaceService,
        IResourceAuthorizationService resourceAuthorizationService,
        IEventSourcedRepository<Space> spaceRepository)
    {
        _spaceService = spaceService;
        _resourceAuthorizationService = resourceAuthorizationService;
        _spaceRepository = spaceRepository;
    }

    public async Task<Unit> Handle(UpdateSpaceCommand request, CancellationToken cancellationToken)
    {
        var space = await _spaceRepository.GetAsync(request.SpaceId, cancellationToken);

        await _resourceAuthorizationService.CheckAsync(space, CommonOperations.Update);

        await _spaceService.UpdateSpaceAsync(space, request.Name, request.Cover, request.Visibility);

        await _spaceRepository.SaveAsync(space, cancellationToken);

        return Unit.Value;
    }
}