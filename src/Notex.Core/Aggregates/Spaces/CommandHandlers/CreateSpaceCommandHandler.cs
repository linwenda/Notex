using MediatR;
using Notex.Core.Aggregates.Spaces.DomainServices;
using Notex.Core.Configuration;
using Notex.Messages.Spaces.Commands;

namespace Notex.Core.Aggregates.Spaces.CommandHandlers;

public class CreateSpaceCommandHandler : IRequestHandler<CreateSpaceCommand, Guid>
{
    private readonly ICurrentUser _currentUser;
    private readonly ISpaceChecker _spaceChecker;
    private readonly IAggregateRepository _aggregateRepository;

    public CreateSpaceCommandHandler(
        ICurrentUser currentUser,
        ISpaceChecker spaceChecker,
        IAggregateRepository aggregateRepository)
    {
        _currentUser = currentUser;
        _spaceChecker = spaceChecker;
        _aggregateRepository = aggregateRepository;
    }

    public async Task<Guid> Handle(CreateSpaceCommand request, CancellationToken cancellationToken)
    {
        var space = Space.Initialize(
            _spaceChecker,
            _currentUser.Id,
            request.Name,
            request.BackgroundImage,
            request.Visibility);

        await _aggregateRepository.SaveAsync(space);

        return space.Id;
    }
}