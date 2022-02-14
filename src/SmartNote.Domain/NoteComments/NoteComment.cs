﻿using SmartNote.Core.Ddd;
using SmartNote.Domain.Notes;

namespace SmartNote.Domain.NoteComments
{
    public sealed class NoteComment : AggregateRoot<Guid>, IHasCreationTime
    {
        public DateTime CreationTime { get; private set; }
        public Guid NoteId { get; private set; }
        public Guid AuthorId { get; private set; }
        public Guid? ReplyToCommentId { get; private set; }
        public string Content { get; private set; }
        public bool IsDeleted { get; private set; }

        private NoteComment()
        {
            //Only for EF
        }

        private NoteComment(Guid noteId, Guid userId, Guid? replyCommentId, string content)
        {
            Id = Guid.NewGuid();
            CreationTime = DateTime.UtcNow;
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

        public static NoteComment Create(Guid noteId, Guid userId, string content)
        {
            return new NoteComment(noteId, userId, null, content);
        }

        public NoteComment Reply(Guid userId, string replyContent)
        {
            if (IsDeleted)
            {
                throw new NoteCommentHasBeenDeletedException();
            }

            return new NoteComment(NoteId, userId, Id, replyContent);
        }

        public void SoftDelete( Guid userId)
        {
            if (IsDeleted) return;

            if (AuthorId != userId)
            {
                throw new OnlyAuthorOfCommentOrNoteMemberCanDeleteException();
            }

            IsDeleted = true;
        }
    }
}