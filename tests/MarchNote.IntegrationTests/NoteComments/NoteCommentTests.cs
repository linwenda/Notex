using System;
using System.Threading.Tasks;
using MarchNote.Application.NoteComments.Commands;
using MarchNote.Application.NoteComments.Queries;
using MarchNote.IntegrationTests.Notes;
using NUnit.Framework;
using Shouldly;

namespace MarchNote.IntegrationTests.NoteComments
{
    using static TestFixture;

    public class NoteCommentTests : TestBase
    {
        [Test]
        public async Task ShouldAddNoteComment()
        {
            var noteId = await NoteTestUtil.CreatePublishedNote();

            var addCommentResponse = await Send(new AddNoteCommentCommand(noteId, "Good Note"));
            addCommentResponse.Code.ShouldBe(20000);

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
            addCommentReplyResponse.Code.ShouldBe(20000);
            
            var getCommentResponse = await Send(new GetNoteCommentByIdQuery(addCommentReplyResponse.Data));
            getCommentResponse.Data.Content.ShouldBe("Reply");
            getCommentResponse.Data.AuthorId.ShouldBe(CurrentUser);
            getCommentResponse.Data.ReplyToCommentId.ShouldBe(commentId);
        }

        private static async Task<Guid> CreateNoteComment()
        {
            var noteId = await NoteTestUtil.CreatePublishedNote();
            var addCommentResponse = await Send(new AddNoteCommentCommand(noteId, "Good Note"));
            return addCommentResponse.Data;
        }
    }
}