using System;
using MarchNote.Domain.NoteComments.Events;
using MarchNote.Domain.Notes;
using MarchNote.Domain.SeedWork;

namespace MarchNote.Domain.NoteComments
{
    public sealed class NoteComment : Entity<Guid>
    {
        public DateTime CreatedAt { get; private set; }
        public NoteId NoteId { get; private set; }
        public Guid AuthorId { get; private set; }
        public Guid? ReplyToCommentId { get; private set; }
        public string Content { get; private set; }
        public bool IsDeleted { get; private set; }

        private NoteComment()
        {
            //Only for EF
        }

        private NoteComment(NoteId noteId, Guid userId, Guid? replyCommentId, string content)
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            NoteId = noteId;
            AuthorId = userId;
            ReplyToCommentId = replyCommentId;
            Content = content;

            if (replyCommentId == null)
            {
                AddDomainEvent(new NoteCommentAddedEvent(Id, content));
            }
            else
            {
                AddDomainEvent(new ReplayToNoteCommentAddedEvent(Id, replyCommentId, Content));
            }
        }

        public static NoteComment Create(NoteId noteId, Guid userId, string content)
        {
            return new NoteComment(noteId, userId, null, content);
        }

        public NoteComment Reply(Guid userId, string replyContent)
        {
            if (IsDeleted)
            {
                throw new NoteCommentException("The comment has been deleted");
            }

            return new NoteComment(NoteId, userId, Id, replyContent);
        }

        public void SoftDelete(Guid userId, NoteMemberGroup memberList)
        {
            if (IsDeleted) return;

            if (AuthorId != userId && !memberList.IsMember(userId))
            {
                throw new NoteCommentException("Only author of comment or note member can delete comment");
            }

            IsDeleted = true;
        }
    }
}