using MediatR;
using Notex.Core.Aggregates.Spaces.DomainServices;
using Notex.Core.Authorization;
using Notex.Core.Configuration;
using Notex.Messages.Spaces.Commands;

namespace Notex.Core.Aggregates.Spaces.CommandHandlers;

public class UpdateSpaceCommandHandler : ICommandHandler<UpdateSpaceCommand>
{
    private readonly IAggregateRepository _aggregateRepository;
    private readonly ISpaceChecker _spaceValidatorService;
    private readonly IResourceAuthorizationService _resourceAuthorizationService;

    public UpdateSpaceCommandHandler(
        IAggregateRepository aggregateRepository,
        ISpaceChecker spaceValidatorService,
        IResourceAuthorizationService resourceAuthorizationService)
    {
        _aggregateRepository = aggregateRepository;
        _spaceValidatorService = spaceValidatorService;
        _resourceAuthorizationService = resourceAuthorizationService;
    }

    public async Task<Unit> Handle(UpdateSpaceCommand request, CancellationToken cancellationToken)
    {
        var space = await _aggregateRepository.LoadAsync<Space>(request.SpaceId);

        await _resourceAuthorizationService.CheckAsync(space, CommonOperations.Update);

        space.Update(
            _spaceValidatorService,
            request.Name,
            request.BackgroundImage,
            request.Visibility);

        await _aggregateRepository.SaveAsync(space);

        return Unit.Value;
    }
}