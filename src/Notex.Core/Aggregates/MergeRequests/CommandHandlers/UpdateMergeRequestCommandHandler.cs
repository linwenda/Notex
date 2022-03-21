using MediatR;
using Notex.Core.Configuration;
using Notex.Messages.MergeRequests.Commands;

namespace Notex.Core.Aggregates.MergeRequests.CommandHandlers;

public class UpdateMergeRequestCommandHandler : ICommandHandler<UpdateMergeRequestCommand>
{
    private readonly ICurrentUser _currentUser;
    private readonly IAggregateRepository _aggregateRepository;

    public UpdateMergeRequestCommandHandler(ICurrentUser currentUser, IAggregateRepository aggregateRepository)
    {
        _currentUser = currentUser;
        _aggregateRepository = aggregateRepository;
    }

    public async Task<Unit> Handle(UpdateMergeRequestCommand request, CancellationToken cancellationToken)
    {
        var mergeRequest = await _aggregateRepository.LoadAsync<MergeRequest>(request.MergeRequestId);

        mergeRequest.Update(_currentUser.Id, request.Title, request.Description);

        await _aggregateRepository.SaveAsync(mergeRequest);

        return Unit.Value;
    }
}