using MediatR;
using Notex.Core.Domain.SeedWork;
using Notex.Core.Domain.Spaces;
using Notex.Core.Identity;
using Notex.Messages.Spaces.Commands;

namespace Notex.Core.Handlers.CommandHandlers;

public class CreateSpaceCommandHandler : IRequestHandler<CreateSpaceCommand, Guid>
{
    private readonly ICurrentUser _currentUser;
    private readonly ISpaceService _spaceService;
    private readonly IEventSourcedRepository<Space> _spaceRepository;

    public CreateSpaceCommandHandler(
        ICurrentUser currentUser,
        ISpaceService spaceService,
        IEventSourcedRepository<Space> spaceRepository)
    {
        _currentUser = currentUser;
        _spaceService = spaceService;
        _spaceRepository = spaceRepository;
    }

    public async Task<Guid> Handle(CreateSpaceCommand request, CancellationToken cancellationToken)
    {
        var space = await _spaceService.CreateSpaceAsync(
            _currentUser.Id,
            request.Name,
            request.BackgroundImage,
            request.Visibility);

        await _spaceRepository.SaveAsync(space, cancellationToken);

        return space.Id;
    }
}