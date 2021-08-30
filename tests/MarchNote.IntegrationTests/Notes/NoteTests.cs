using System;
using System.Linq;
using System.Threading.Tasks;
using MarchNote.Application.Notes.Commands;
using MarchNote.Application.Notes.Queries;
using MarchNote.Application.Spaces.Commands;
using MarchNote.Domain.Notes;
using NUnit.Framework;
using Shouldly;

namespace MarchNote.IntegrationTests.Notes
{
    using static TestFixture;

    public class NoteTests : TestBase
    {
        [Test]
        public async Task ShouldCreateNote()
        {
            var createSpaceResponse = await Send(new CreateSpaceCommand
            {
                Color = "#FFF",
                Icon = "Icon",
                Name = "Default"
            });
            
            var command = new CreateNoteCommand
            {
                SpaceId = createSpaceResponse.Data,
                Title = "Test Note",
                Content = "Test Content",
            };
            var commandResponse = await Send(command);

            var queryResponse = await Send(new GetNoteQuery(commandResponse.Data));
            queryResponse.ShouldNotBeNull();
            queryResponse.Data.Title.ShouldBe(command.Title);
            queryResponse.Data.Content.ShouldBe(command.Content);
            queryResponse.Data.AuthorId.ShouldBe(CurrentUser);
            queryResponse.Data.Status.ShouldBe(NoteStatus.Draft);
        }

        [Test]
        public async Task ShouldEditDraftNote()
        {
            var noteId = await CreateTestNote();

            var editCommand = new EditNoteCommand(noteId, "Title#2", "Content#2");
            await Send(editCommand);

            var queryResponse = await Send(new GetNoteQuery(noteId));
            queryResponse.ShouldNotBeNull();
            queryResponse.Data.Title.ShouldBe(editCommand.Title);
            queryResponse.Data.Content.ShouldBe(editCommand.Content);
            queryResponse.Data.Version.ShouldBe(1);
        }

        [Test]
        public async Task ShouldPublishDraftNote()
        {
            var noteId = await CreateTestNote();

            await Send(new PublishNoteCommand(noteId));

            var noteQueryResponse = await Send(new GetNoteQuery(noteId));
            noteQueryResponse.ShouldNotBeNull();
            noteQueryResponse.Data.Status.ShouldBe(NoteStatus.Published);
            noteQueryResponse.Data.Version.ShouldBe(1);

            var noteHistoryResponse = await Send(new GetNoteHistoriesQuery(noteId));
            noteHistoryResponse.Data.Count().ShouldBe(1);
        }

        [Test]
        public async Task ShouldUpdateNewVersionWhenEditPublishedNote()
        {
            var noteId = await CreateTestNote();

            await Send(new PublishNoteCommand(noteId));

            var command = new EditNoteCommand(noteId, "Title#2", "Content#2");
            var commandResponse = await Send(command);

            var queryResponse = await Send(new GetNoteQuery(noteId));
            queryResponse.ShouldNotBeNull();
            queryResponse.Data.Title.ShouldBe(command.Title);
            queryResponse.Data.Content.ShouldBe(command.Content);
            queryResponse.Data.Version.ShouldBe(2);

            var noteHistoryResponse = await Send(new GetNoteHistoriesQuery(noteId));
            noteHistoryResponse.Data.Count().ShouldBe(2);
        }

        [Test]
        public async Task ShouldDeleteNote()
        {
            var noteId = await CreateTestNote();

            await Send(new DeleteNoteCommand(noteId));

            var queryResponse = await Send(new GetNoteQuery(noteId));
            queryResponse.Data.ShouldBeNull();
        }

        [Test]
        public async Task ShouldDraftOutPublishedNote()
        {
            var noteId = await CreateTestNote();
            
            await Send(new PublishNoteCommand(noteId));
            
            var command = new DraftOutNoteCommand(noteId);
            var commandResponse = await Send(command);
            
            var queryResponse = await Send(new GetNoteQuery(commandResponse.Data));
            queryResponse.ShouldNotBeNull();
            queryResponse.Data.FromId.ShouldBe(noteId);
            queryResponse.Data.Version.ShouldBe(1);
        }
        
        private static async Task<Guid> CreateTestNote()
        {
            var createSpaceResponse = await Send(new CreateSpaceCommand
            {
                Color = "#FFF",
                Icon = "Icon",
                Name = "Default"
            });
            
            var command = new CreateNoteCommand
            {
                SpaceId = createSpaceResponse.Data,
                Title = "Test Note",
                Content = "Test Content"
            };
            var commandResponse = await Send(command);

            return commandResponse.Data;
        }
    }
}