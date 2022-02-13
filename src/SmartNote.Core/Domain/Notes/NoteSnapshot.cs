using SmartNote.Core.Domain.Notes.Blocks;

namespace SmartNote.Core.Domain.Notes
{
    public class NoteSnapshot : Snapshot<NoteId>
    {
        public Guid? FromId { get; }
        public Guid AuthorId { get; }
        public string Title { get; }
        public bool IsDeleted { get; }
        public NoteStatus Status { get; }
        public List<Block> Blocks { get; }

        public NoteSnapshot(
            NoteId noteId,
            int aggregateVersion,
            Guid? fromId,
            Guid authorId,
            string title,
            List<Block> blocks,
            bool isDeleted,
            NoteStatus status) : base(noteId, aggregateVersion)
        {
            FromId = fromId;
            AuthorId = authorId;
            Title = title;
            Blocks = blocks;
            IsDeleted = isDeleted;
            Status = status;
        }
    }
}