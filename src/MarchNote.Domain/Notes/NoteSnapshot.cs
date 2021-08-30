using System;
using System.Collections.Generic;
using MarchNote.Domain.SeedWork;

namespace MarchNote.Domain.Notes
{
    public class NoteSnapshot : Snapshot
    {
        public Guid? FromId { get; }
        public Guid AuthorId { get; }
        public string Title { get; }
        public string Content { get; }
        public bool IsDeleted { get; }
        public NoteStatus Status { get; }
        public List<NoteMemberSnapshot> MemberList { get; }

        public NoteSnapshot(
            Guid aggregateId,
            int aggregateVersion,
            Guid? fromId,
            Guid authorId,
            string title,
            string content,
            bool isDeleted,
            NoteStatus status,
            List<NoteMemberSnapshot> memberList) : base(aggregateId, aggregateVersion)
        {
            FromId = fromId;
            AuthorId = authorId;
            Title = title;
            Content = content;
            IsDeleted = isDeleted;
            Status = status;
            MemberList = memberList;
        }
    }
}