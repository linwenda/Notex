using Notex.Core.Domain.MergeRequests;
using Notex.Core.Domain.Notes;
using Notex.Core.Domain.SeedWork;
using Notex.Core.Identity;
using Notex.Messages.MergeRequests.Commands;

namespace Notex.Core.Handlers.CommandHandlers;

public class CreateMergeRequestCommandHandler : ICommandHandler<CreateMergeRequestCommand, Guid>
{
    private readonly ICurrentUser _currentUser;
    private readonly IMergeRequestService _mergeRequestService;
    private readonly IEventSourcedRepository<Note> _noteRepository;
    private readonly IEventSourcedRepository<MergeRequest> _mergeRequestRepository;

    public CreateMergeRequestCommandHandler(
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

    public async Task<Guid> Handle(CreateMergeRequestCommand request, CancellationToken cancellationToken)
    {
        var note = await _noteRepository.GetAsync(request.NoteId, cancellationToken);
        
        var mergeRequest = await _mergeRequestService.CreateMergeRequestAsync(
            note,
            _currentUser.Id,
            request.Title,
            request.Description);

        await _mergeRequestRepository.SaveAsync(mergeRequest, cancellationToken);

        return mergeRequest.Id;
    }
}