﻿using System;
using System.Collections.Generic;
using MarchNote.Domain.NoteComments;
using MarchNote.Domain.NoteComments.Events;
using MarchNote.Domain.NoteComments.Exceptions;
using MarchNote.Domain.Notes;
using MarchNote.Domain.Shared;
using MarchNote.UnitTests.Notes;
using NUnit.Framework;
using Shouldly;

namespace MarchNote.UnitTests.NoteComments
{
    public class NoteCommentTests : TestBase
    {
        private Note _note;
        private NoteComment _comment;

        [SetUp]
        public void SetUp()
        {
            var data = NoteTestUtil.CreatePublishedNote();

            _note = data.Note;
            _comment = _note.AddComment(Guid.NewGuid(), "comment");
        }

        [Test]
        public void AddComment_IsSuccessful()
        {
            var userId = Guid.NewGuid();
            var comment = _note.AddComment(userId, "comment");

            comment.AuthorId.ShouldBe(userId);
            comment.IsDeleted.ShouldBeFalse();
            comment.NoteId.ShouldBe(_note.Id);
        }

        [Test]
        public void Reply_Comment_IsSuccessful()
        {
            var replyUserId = Guid.NewGuid();
            var replyComment = _comment.Reply(replyUserId, "reply");
            replyComment.AuthorId.ShouldBe(replyUserId);
            replyComment.Content.ShouldBe("reply");
            replyComment.DomainEvents.ShouldHaveEvent<ReplayToNoteCommentAddedEvent>();
        }

        [Test]
        public void Delete_ByAuthor_IsSuccessful()
        {
            _comment.SoftDelete(_comment.AuthorId, new NoteMemberGroup(new List<NoteMember>()));
            _comment.IsDeleted.ShouldBeTrue();
        }

        [Test]
        public void Delete_ByOtherUser_ThrowException()
        {
            Should.Throw<OnlyAuthorOfCommentOrNoteMemberCanDeleteException>(() =>
                _comment.SoftDelete(Guid.NewGuid(), new NoteMemberGroup(new List<NoteMember>())));
        }
    }
}