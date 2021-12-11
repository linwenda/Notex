namespace SmartNote.Core.Domain.NoteMergeRequests.Events
{
    public class NoteMergeRequestMergedEvent : DomainEventBase
    {
        public Guid NoteMergeRequestId { get; }
        public Guid NoteId { get; }

        public NoteMergeRequestMergedEvent(Guid noteMergeRequestId, Guid noteId)
        {
            NoteMergeRequestId = noteMergeRequestId;
            NoteId = noteId;
        }
    }
}