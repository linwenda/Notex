using System;

namespace MarchNote.Application.NoteComments.Queries
{
    public class NoteCommentDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid NoteId { get; set; }
        public Guid AuthorId { get; set; }
        public Guid? ReplayToCommentId { get; set; }
        public string Content { get; set; }
    }
}