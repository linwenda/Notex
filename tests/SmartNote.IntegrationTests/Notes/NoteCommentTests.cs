using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Shouldly;
using SmartNote.Application.NoteComments.Commands;
using SmartNote.Application.NoteComments.Queries;

namespace SmartNote.IntegrationTests.Notes
{
    using static TestFixture;

    public class NoteCommentTests : TestBase
    {
        [Test]
        public async Task ShouldAddNoteComment()
        {
            var noteId = await NoteTestUtil.CreatePublishedNote();

            var addCommentResponse = await Send(new AddNoteCommentCommand(noteId, "Good Note"));
            var getCommentResponse = await Send(new GetNoteCommentByIdQuery(addCommentResponse));

            getCommentResponse.NoteId.ShouldBe(noteId);
            getCommentResponse.Content.ShouldBe("Good Note");
            getCommentResponse.AuthorId.ShouldBe(CurrentUser);
            getCommentResponse.ReplyToCommentId.ShouldBeNull();
        }

        [Test]
        public async Task ShouldAddCommentReply()
        {
            var commentId = await CreateNoteComment();
            var addCommentReplyResponse = await Send(new AddNoteCommentReplyCommand(commentId, "Reply"));
            var getCommentResponse = await Send(new GetNoteCommentByIdQuery(addCommentReplyResponse));

            getCommentResponse.Content.ShouldBe("Reply");
            getCommentResponse.AuthorId.ShouldBe(CurrentUser);
            getCommentResponse.ReplyToCommentId.ShouldBe(commentId);
        }

        [Test]
        public async Task ShouldDeleteComment()
        {
            var commentId = await CreateNoteComment();
            await Send(new DeleteNoteCommentCommand(commentId));

            var getCommentResponse = await Send(new GetNoteCommentByIdQuery(commentId));
            getCommentResponse.ShouldBeNull();
        }

        private static async Task<Guid> CreateNoteComment()
        {
            var noteId = await NoteTestUtil.CreatePublishedNote();
            var addCommentResponse = await Send(new AddNoteCommentCommand(noteId, "Good Note"));
            return addCommentResponse;
        }
    }
}