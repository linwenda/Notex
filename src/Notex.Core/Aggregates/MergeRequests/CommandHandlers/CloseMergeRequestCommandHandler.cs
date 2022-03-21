using MediatR;
using Notex.Core.Configuration;
using Notex.Messages.MergeRequests.Commands;

namespace Notex.Core.Aggregates.MergeRequests.CommandHandlers;

public class CloseMergeRequestCommandHandler : ICommandHandler<CloseMergeRequestCommand>
{
    private readonly ICurrentUser _currentUser;
    private readonly IAggregateRepository _aggregateRepository;

    public CloseMergeRequestCommandHandler(ICurrentUser currentUser, IAggregateRepository aggregateRepository)
    {
        _currentUser = currentUser;
        _aggregateRepository = aggregateRepository;
    }

    public async Task<Unit> Handle(CloseMergeRequestCommand request, CancellationToken cancellationToken)
    {
        var mergeRequest = await _aggregateRepository.LoadAsync<MergeRequest>(request.MergeRequestId);

        mergeRequest.Close(_currentUser.Id);

        await _aggregateRepository.SaveAsync(mergeRequest);

        return Unit.Value;
    }
}