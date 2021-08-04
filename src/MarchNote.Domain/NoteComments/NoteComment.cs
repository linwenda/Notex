using System;
using MarchNote.Domain.NoteAggregate;
using MarchNote.Domain.NoteComments.Events;
using MarchNote.Domain.SeedWork;
using MarchNote.Domain.Users;

namespace MarchNote.Domain.NoteComments
{
    public class NoteComment : Entity
    {
        public NoteCommentId Id { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public NoteId NoteId { get; private set; }
        public UserId AuthorId { get; private set; }
        public NoteCommentId ReplyToCommentId { get; private set; }
        public string Content { get; private set; }
        public bool IsDeleted { get; private set; }

        private NoteComment()
        {
            //Only for EF
        }

        private NoteComment(NoteId noteId, UserId userId, NoteCommentId replyCommentId, string content)
        {
            Id = new NoteCommentId(Guid.NewGuid());
            CreatedAt = DateTime.UtcNow;
            NoteId = noteId;
            AuthorId = userId;
            ReplyToCommentId = replyCommentId;
            Content = content;

            if (replyCommentId == null)
            {
                AddDomainEvent(new NoteCommentAddedEvent(Id.Value, content));
            }
            else
            {
                AddDomainEvent(new ReplayToNoteCommentAddedEvent(Id.Value, replyCommentId.Value, Content));
            }
        }

        public static NoteComment Create(NoteId noteId, UserId userId, string content)
        {
            return new NoteComment(noteId, userId, null, content);
        }

        public NoteComment Reply(UserId userId, string replyContent)
        {
            if (IsDeleted)
            {
                throw new BusinessException(ExceptionCode.CommentHasBeenDeleted, "The comment has been deleted");
            }

            return new NoteComment(NoteId, userId, Id, replyContent);
        }

        public void SoftDelete(UserId userId, NoteMemberGroup memberList)
        {
            if (IsDeleted) return;

            if (AuthorId != userId && !memberList.IsMember(userId))
            {
                throw new BusinessException(ExceptionCode.CommentCanBeDeletedOnlyByAuthorOrMember,
                    "Only author of comment or note member can delete comment.");
            }

            IsDeleted = true;
        }
    }
}