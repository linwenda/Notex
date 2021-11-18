using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MarchNote.Domain.NoteComments;
using MarchNote.Domain.NoteComments.Events;
using MarchNote.Domain.NoteComments.Exceptions;
using MarchNote.Domain.Notes;
using MarchNote.Domain.Shared;
using MarchNote.UnitTests.Notes;
using NSubstitute;
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
            comment.NoteId.ShouldBe(_note.Id.Value);
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
        public async Task Delete_ByAuthor_IsSuccessful()
        {
            var noteChecker = Substitute.For<INoteChecker>();
            noteChecker.IsAuthorAsync(Arg.Any<Guid>(), Arg.Any<Guid>()).Returns(true);

            await _comment.SoftDeleteAsync(noteChecker, _comment.AuthorId);
            _comment.IsDeleted.ShouldBeTrue();
        }

        [Test]
        public void Delete_ByOtherUser_ThrowException()
        {
            var noteChecker = Substitute.For<INoteChecker>();
            noteChecker.IsAuthorAsync(Arg.Any<Guid>(), Arg.Any<Guid>()).Returns(false);

            Should.ThrowAsync<OnlyAuthorOfCommentOrNoteMemberCanDeleteException>(async () =>
                await _comment.SoftDeleteAsync(noteChecker, Guid.NewGuid()));
        }
    }
}