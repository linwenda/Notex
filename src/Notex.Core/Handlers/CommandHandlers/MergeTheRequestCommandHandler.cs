using MediatR;
using Notex.Core.Domain.MergeRequests;
using Notex.Core.Domain.Notes;
using Notex.Core.Domain.SeedWork;
using Notex.Core.Identity;
using Notex.Messages.MergeRequests.Commands;

namespace Notex.Core.Handlers.CommandHandlers;

public class MergeTheRequestCommandHandler : ICommandHandler<MergeTheRequestCommand>
{
    private readonly ICurrentUser _currentUser;
    private readonly IMergeRequestService _mergeRequestService;
    private readonly IEventSourcedRepository<Note> _noteRepository;
    private readonly IEventSourcedRepository<MergeRequest> _mergeRequestRepository;

    public MergeTheRequestCommandHandler(
        ICurrentUser currentUser,
        IMergeRequestService mergeRequestService,
        IEventSourcedRepository<Note> noteRepository,
        IEventSourcedRepository<MergeRequest> mergeRequestRepository)
    {
        _currentUser = currentUser;
        _mergeRequestService = mergeRequestService;
        _noteRepository = noteRepository;
        _mergeRequestRepository = mergeRequestRepository;
    }

    public async Task<Unit> Handle(MergeTheRequestCommand request, CancellationToken cancellationToken)
    {
        var mergeRequest = await _mergeRequestRepository.GetAsync(request.MergeRequestId, cancellationToken);

        var sourceNote = await _noteRepository.GetAsync(mergeRequest.GetSourceNoteId(), cancellationToken);

        var destinationNote = await _noteRepository.GetAsync(mergeRequest.GetDestinationNoteId(), cancellationToken);

        _mergeRequestService.Merge(mergeRequest, sourceNote, destinationNote, _currentUser.Id);

        await _mergeRequestRepository.SaveAsync(mergeRequest, cancellationToken);

        return Unit.Value;
    }
}