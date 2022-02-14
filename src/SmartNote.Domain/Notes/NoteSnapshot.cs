using SmartNote.Core.Ddd;
using SmartNote.Domain.Notes.Blocks;

namespace SmartNote.Domain.Notes
{
    public class NoteSnapshot : Snapshot<NoteId>
    {
        public Guid? FromId { get; }
        public Guid AuthorId { get; }
        public string Title { get; }
        public bool IsDeleted { get; }
        public NoteStatus Status { get; }
        public List<Block> Content { get; }

        public NoteSnapshot(
            NoteId noteId,
            int noteVersion,
            Guid? fromId,
            Guid authorId,
            string title,
            List<Block> content,
            bool isDeleted,
            NoteStatus status) : base(noteId, noteVersion)
        {
            FromId = fromId;
            AuthorId = authorId;
            Title = title;
            Content = content;
            IsDeleted = isDeleted;
            Status = status;
        }
    }
}