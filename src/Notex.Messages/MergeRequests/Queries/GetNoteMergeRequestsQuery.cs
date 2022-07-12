namespace Notex.Messages.MergeRequests.Queries;

public class GetNoteMergeRequestsQuery : IQuery<IEnumerable<MergeRequestDto>>
{
    public Guid NoteId { get; }

    public GetNoteMergeRequestsQuery(Guid noteId)
    {
        NoteId = noteId;
    }
}