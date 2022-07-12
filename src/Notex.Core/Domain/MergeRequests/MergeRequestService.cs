using Notex.Core.Domain.MergeRequests.Exceptions;
using Notex.Core.Domain.MergeRequests.ReadModels;
using Notex.Core.Domain.Notes;
using Notex.Core.Domain.SeedWork;
using Notex.Messages.MergeRequests;

namespace Notex.Core.Domain.MergeRequests;

public class MergeRequestService : IMergeRequestService
{
    private readonly IReadOnlyRepository<MergeRequestDetail> _mergeRequestRepository;

    public MergeRequestService(IReadOnlyRepository<MergeRequestDetail> mergeRequestRepository)
    {
        _mergeRequestRepository = mergeRequestRepository;
    }

    public async Task<MergeRequest> CreateMergeRequestAsync(Note note, Guid userId, string title, string description)
    {
        note.CheckPublish();

        var cloneFromId = note.GetCloneFromId();

        if (!cloneFromId.HasValue)
        {
            throw new OnlyClonedNoteCanCreateMergeRequestException();
        }

        var hasOpenMergeRequest = await _mergeRequestRepository.AnyAsync(m =>
            m.SourceNoteId == note.Id &&
            m.DestinationNoteId == cloneFromId &&
            m.Status == MergeRequestStatus.Open);

        if (hasOpenMergeRequest)
        {
            throw new MergeRequestHasBeenExistsException();
        }

        return MergeRequest.Initialize(
            userId,
            note.Id,
            cloneFromId.Value,
            title, description);
    }

    public void ReOpenMergeRequest(MergeRequest mergeRequest, Note destinationNote, Guid userId)
    {
        destinationNote.CheckPublish();

        mergeRequest.ReOpen(userId);
    }

    public void Merge(MergeRequest mergeRequest, Note sourceNote, Note destinationNote, Guid userId)
    {
        sourceNote.CheckPublish();
 
        destinationNote.CheckPublish();
        
        mergeRequest.SetMergeStatus(userId);
    }
}