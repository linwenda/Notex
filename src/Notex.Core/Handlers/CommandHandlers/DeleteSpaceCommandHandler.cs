using MediatR;
using Notex.Core.Domain.SeedWork;
using Notex.Core.Domain.Spaces;
using Notex.Core.Identity;
using Notex.Messages.Spaces.Commands;

namespace Notex.Core.Handlers.CommandHandlers;

public class DeleteSpaceCommandHandler : ICommandHandler<DeleteSpaceCommand>
{
    private readonly IEventSourcedRepository<Space> _spaceRepository;
    private readonly IResourceAuthorizationService _resourceAuthorizationService;

    public DeleteSpaceCommandHandler(
        IEventSourcedRepository<Space> spaceRepository,
        IResourceAuthorizationService authorizationService)
    {
        _spaceRepository = spaceRepository;
        _resourceAuthorizationService = authorizationService;
    }

    public async Task<Unit> Handle(DeleteSpaceCommand request, CancellationToken cancellationToken)
    {
        var space = await _spaceRepository.GetAsync(request.SpaceId, cancellationToken);

        await _resourceAuthorizationService.CheckAsync(space, CommonOperations.Delete);

        space.Delete();

        await _spaceRepository.SaveAsync(space, cancellationToken);

        return Unit.Value;
    }
}