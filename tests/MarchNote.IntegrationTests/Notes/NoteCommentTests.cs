using System;
using System.Threading.Tasks;
using MarchNote.Application.NoteComments.Commands;
using MarchNote.Application.NoteComments.Queries;
using NUnit.Framework;
using Shouldly;

namespace MarchNote.IntegrationTests.Notes
{
    using static TestFixture;

    public class NoteCommentTests : TestBase
    {
        [Test]
        public async Task ShouldAddNoteComment()
        {
            var noteId = await NoteTestUtil.CreatePublishedNote();

            var addCommentResponse = await Send(new AddNoteCommentCommand(noteId, "Good Note"));
            var getCommentResponse = await Send(new GetNoteCommentByIdQuery(addCommentResponse.Data));
            
            getCommentResponse.Data.NoteId.ShouldBe(noteId);
            getCommentResponse.Data.Content.ShouldBe("Good Note");
            getCommentResponse.Data.AuthorId.ShouldBe(CurrentUser);
            getCommentResponse.Data.ReplyToCommentId.ShouldBeNull();
        }

        [Test]
        public async Task ShouldAddCommentReply()
        {
            var commentId = await CreateNoteComment();
            var addCommentReplyResponse = await Send(new AddNoteCommentReplyCommand(commentId, "Reply"));
            var getCommentResponse = await Send(new GetNoteCommentByIdQuery(addCommentReplyResponse.Data));
            
            getCommentResponse.Data.Content.ShouldBe("Reply");
            getCommentResponse.Data.AuthorId.ShouldBe(CurrentUser);
            getCommentResponse.Data.ReplyToCommentId.ShouldBe(commentId);
        }

        [Test]
        public async Task ShouldDeleteComment()
        {
            var commentId = await CreateNoteComment();
            await Send(new DeleteNoteCommentCommand(commentId));

            var getCommentResponse = await Send(new GetNoteCommentByIdQuery(commentId));
            getCommentResponse.Data.ShouldBeNull();
        }

        private static async Task<Guid> CreateNoteComment()
        {
            var noteId = await NoteTestUtil.CreatePublishedNote();
            var addCommentResponse = await Send(new AddNoteCommentCommand(noteId, "Good Note"));
            return addCommentResponse.Data;
        }
    }
}