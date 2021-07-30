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
            getCommentResponse.Data.ReplayToCommentId.ShouldBeNull();
        }
    }
}