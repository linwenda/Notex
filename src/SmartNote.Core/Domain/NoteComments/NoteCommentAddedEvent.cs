namespace SmartNote.Core.Domain.NoteComments
{
    public class NoteCommentAddedEvent : DomainEventBase
    {
        public Guid CommentId { get; }
        public string Content { get; }

        public NoteCommentAddedEvent(Guid commentId, string content)
        {
            CommentId = commentId;
            Content = content;
        }
    }
}