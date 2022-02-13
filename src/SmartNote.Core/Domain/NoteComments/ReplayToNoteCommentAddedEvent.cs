﻿namespace SmartNote.Core.Domain.NoteComments
{
    public class ReplayToNoteCommentAddedEvent : DomainEventBase
    {
        public Guid CommentId { get; }
        public Guid? ReplayToCommentId { get; }
        public string Content { get; }

        public ReplayToNoteCommentAddedEvent(Guid commentId, Guid? replayToCommentId, string content)
        {
            CommentId = commentId;
            ReplayToCommentId = replayToCommentId;
            Content = content;
        }
    }
}