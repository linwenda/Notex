namespace SmartNote.Core.Application.NoteComments.Contracts
{
    public class NoteCommentDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid NoteId { get; set; }
        public Guid AuthorId { get; set; }
        public Guid? ReplyToCommentId { get; set; }
        public string Content { get; set; }
    }
}