using MediatR;
using Notex.Core.Aggregates.Notes.DomainServices;
using Notex.Core.Configuration;
using Notex.Messages.MergeRequests.Commands;

namespace Notex.Core.Aggregates.MergeRequests.CommandHandlers;

public class ReopenMergeRequestCommandHandler : ICommandHandler<ReopenMergeRequestCommand>
{
    private readonly ICurrentUser _currentUser;
    private readonly INoteChecker _noteChecker;
    private readonly IAggregateRepository _aggregateRepository;

    public ReopenMergeRequestCommandHandler(
        ICurrentUser currentUser,
        INoteChecker noteChecker,
        IAggregateRepository aggregateRepository)
    {
        _currentUser = currentUser;
        _noteChecker = noteChecker;
        _aggregateRepository = aggregateRepository;
    }

    public async Task<Unit> Handle(ReopenMergeRequestCommand request, CancellationToken cancellationToken)
    {
        var mergeRequest = await _aggregateRepository.LoadAsync<MergeRequest>(request.MergeRequestId);

        mergeRequest.Reopen(_noteChecker,_currentUser.Id);

        await _aggregateRepository.SaveAsync(mergeRequest);

        return Unit.Value;
    }
}