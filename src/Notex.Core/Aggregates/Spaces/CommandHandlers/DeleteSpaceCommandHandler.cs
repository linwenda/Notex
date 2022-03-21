using MediatR;
using Notex.Core.Authorization;
using Notex.Core.Configuration;
using Notex.Messages.Spaces.Commands;

namespace Notex.Core.Aggregates.Spaces.CommandHandlers;

public class DeleteSpaceCommandHandler : ICommandHandler<DeleteSpaceCommand>
{
    private readonly IAggregateRepository _aggregateRepository;
    private readonly IResourceAuthorizationService _resourceAuthorizationService;

    public DeleteSpaceCommandHandler(
        IAggregateRepository aggregateRepository,
        IResourceAuthorizationService authorizationService)
    {
        _aggregateRepository = aggregateRepository;
        _resourceAuthorizationService = authorizationService;
    }

    public async Task<Unit> Handle(DeleteSpaceCommand request, CancellationToken cancellationToken)
    {
        var space = await _aggregateRepository.LoadAsync<Space>(request.SpaceId);

        await _resourceAuthorizationService.CheckAsync(space, CommonOperations.Delete);

        space.Delete();

        await _aggregateRepository.SaveAsync(space);

        return Unit.Value;
    }
}