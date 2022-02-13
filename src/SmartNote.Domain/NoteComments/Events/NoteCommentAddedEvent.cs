﻿namespace SmartNote.Domain.NoteComments.Events
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