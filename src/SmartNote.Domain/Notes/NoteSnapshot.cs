using SmartNote.Domain.Notes.Blocks;

namespace SmartNote.Domain.Notes
{
    public class NoteSnapshot : Snapshot
    {
        public Guid? FromId { get; }
        public Guid AuthorId { get; }
        public string Title { get; }
        public bool IsDeleted { get; }
        public NoteStatus Status { get; }
        public List<NoteMemberSnapshot> MemberList { get; }
        public List<Block> Blocks { get; }

        public NoteSnapshot(
            Guid aggregateId,
            int aggregateVersion,
            Guid? fromId,
            Guid authorId,
            string title,
            List<Block> blocks,
            bool isDeleted,
            NoteStatus status,
            List<NoteMemberSnapshot> memberList) : base(aggregateId, aggregateVersion)
        {
            FromId = fromId;
            AuthorId = authorId;
            Title = title;
            Blocks = blocks;
            IsDeleted = isDeleted;
            Status = status;
            MemberList = memberList;
        }
    }
}