using System;
using System.Collections.Generic;
using MarchNote.Domain.NoteComments;
using MarchNote.Domain.NoteComments.Events;
using MarchNote.Domain.Notes;
using MarchNote.Domain.SeedWork;
using MarchNote.Domain.Users;
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
            _comment = _note.AddComment(new UserId(Guid.NewGuid()), "comment");
        }

        [Test]
        public void AddComment_IsSuccessful()
        {
            var userId = new UserId(Guid.NewGuid());
            var comment = _note.AddComment(userId, "comment");

            comment.AuthorId.ShouldBe(userId);
            comment.IsDeleted.ShouldBeFalse();
            comment.NoteId.ShouldBe(_note.Id);
        }

        [Test]
        public void Reply_Comment_IsSuccessful()
        {
            var replyUserId = new UserId(Guid.NewGuid());
            var replyComment = _comment.Reply(replyUserId, "reply");
            replyComment.AuthorId.ShouldBe(replyUserId);
            replyComment.Content.ShouldBe("reply");
            replyComment.DomainEvents.ShouldHaveEvent<ReplayToNoteCommentAddedEvent>();
        }

        [Test]
        public void Reply_CommentHasBeenDeleted_ThrowException()
        {
            _comment.SoftDelete(_comment.AuthorId, new NoteMemberGroup(new List<NoteMember>()));

            ShouldThrowBusinessException(() => _comment.Reply(new UserId(Guid.NewGuid()), "reply"),
                ExceptionCode.NoteCommentException,
                "The comment has been deleted");
        }

        [Test]
        public void Delete_ByAuthor_IsSuccessful()
        {
            _comment.SoftDelete(_comment.AuthorId, new NoteMemberGroup(new List<NoteMember>()));
            _comment.IsDeleted.ShouldBeTrue();
        }

        [Test]
        public void Delete_ByOther_ThrowException()
        {
            ShouldThrowBusinessException(() =>
                    _comment.SoftDelete(new UserId(Guid.NewGuid()), new NoteMemberGroup(new List<NoteMember>())),
                ExceptionCode.NoteCommentException,
                "Only author of comment or note member can delete comment");
        }
    }
}