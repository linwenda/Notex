using Notex.Core.Aggregates.MergeRequests.DomainServices;
using Notex.Core.Aggregates.Notes;
using Notex.Core.Aggregates.Notes.DomainServices;
using Notex.Core.Configuration;
using Notex.Messages.MergeRequests.Commands;

namespace Notex.Core.Aggregates.MergeRequests.CommandHandlers;

public class CreateMergeRequestCommandHandler : ICommandHandler<CreateMergeRequestCommand, Guid>
{
    private readonly ICurrentUser _currentUser;
    private readonly INoteChecker _noteChecker;
    private readonly IMergeRequestChecker _mergeRequestChecker;
    private readonly IAggregateRepository _aggregateRepository;

    public CreateMergeRequestCommandHandler(
        ICurrentUser currentUser,
        INoteChecker noteChecker,
        IMergeRequestChecker mergeRequestChecker,
        IAggregateRepository aggregateRepository)
    {
        _currentUser = currentUser;
        _noteChecker = noteChecker;
        _mergeRequestChecker = mergeRequestChecker;
        _aggregateRepository = aggregateRepository;
    }

    public async Task<Guid> Handle(CreateMergeRequestCommand request, CancellationToken cancellationToken)
    {
        var note = await _aggregateRepository.LoadAsync<Note>(request.NoteId);

        var mergeRequest = note.CreateMergeRequest(_noteChecker, _mergeRequestChecker, _currentUser.Id, request.Title,
            request.Description);

        await _aggregateRepository.SaveAsync(mergeRequest);

        return mergeRequest.Id;
    }
}