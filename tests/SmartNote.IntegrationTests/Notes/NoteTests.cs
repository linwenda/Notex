using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Shouldly;
using SmartNote.Core.Application.Notes.Contracts;
using SmartNote.Core.Application.Spaces.Contracts;
using SmartNote.Core.Domain.Notes;
using SmartNote.Core.Domain.Spaces;

namespace SmartNote.IntegrationTests.Notes
{
    using static TestFixture;

    public class NoteTests : TestBase
    {
        [Test]
        public async Task ShouldCreateNote()
        {
            var createSpaceResponse = await Send(new CreateSpaceCommand
            {
                BackgroundColor = "#FFF",
                Name = "Default",
                Visibility = Visibility.Public
            });
            
            var command = new CreateNoteCommand
            {
                SpaceId = createSpaceResponse,
                Title = "Test Note",
                Content = "Test Content",
            };
            var commandResponse = await Send(command);

            var queryResponse = await Send(new GetNoteQuery(commandResponse));
            queryResponse.ShouldNotBeNull();
            queryResponse.Title.ShouldBe(command.Title);
            queryResponse.Content.ShouldBe(command.Content);
            queryResponse.AuthorId.ShouldBe(CurrentUser);
            queryResponse.Status.ShouldBe(NoteStatus.Draft);
        }

        [Test]
        public async Task ShouldEditDraftNote()
        {
            var noteId = await CreateTestNote();

            var editCommand = new UpdateNoteCommand(noteId, "Title#2", "Content#2");
            await Send(editCommand);

            var queryResponse = await Send(new GetNoteQuery(noteId));
            queryResponse.ShouldNotBeNull();
            queryResponse.Title.ShouldBe(editCommand.Title);
            queryResponse.Content.ShouldBe(editCommand.Content);
            queryResponse.Version.ShouldBe(1);
        }

        [Test]
        public async Task ShouldPublishDraftNote()
        {
            var noteId = await CreateTestNote();

            await Send(new PublishNoteCommand(noteId));

            var noteQueryResponse = await Send(new GetNoteQuery(noteId));
            noteQueryResponse.ShouldNotBeNull();
            noteQueryResponse.Status.ShouldBe(NoteStatus.Published);
            noteQueryResponse.Version.ShouldBe(1);

            var noteHistoryResponse = await Send(new GetNoteHistoriesQuery(noteId));
            noteHistoryResponse.Count().ShouldBe(1);
        }

        [Test]
        public async Task ShouldUpdateNewVersionWhenEditPublishedNote()
        {
            var noteId = await CreateTestNote();

            await Send(new PublishNoteCommand(noteId));

            var command = new UpdateNoteCommand(noteId, "Title#2", "Content#2");
            await Send(command);

            var queryResponse = await Send(new GetNoteQuery(noteId));
            queryResponse.ShouldNotBeNull();
            queryResponse.Title.ShouldBe(command.Title);
            queryResponse.Content.ShouldBe(command.Content);
            queryResponse.Version.ShouldBe(2);

            var noteHistoryResponse = await Send(new GetNoteHistoriesQuery(noteId));
            noteHistoryResponse.Count().ShouldBe(2);
        }

        [Test]
        public async Task ShouldDeleteNote()
        {
            var noteId = await CreateTestNote();

            await Send(new DeleteNoteCommand(noteId));

            var queryResponse = await Send(new GetNoteQuery(noteId));
            queryResponse.ShouldBeNull();
        }

        [Test]
        public async Task ShouldDraftOutPublishedNote()
        {
            var noteId = await CreateTestNote();
            
            await Send(new PublishNoteCommand(noteId));
            
            var command = new ForkNoteCommand(noteId);
            var commandResponse = await Send(command);
            
            var queryResponse = await Send(new GetNoteQuery(commandResponse));
            queryResponse.ShouldNotBeNull();
            queryResponse.ForkId.ShouldBe(noteId);
            queryResponse.Version.ShouldBe(1);
        }
        
        private static async Task<Guid> CreateTestNote()
        {
            var createSpaceResponse = await Send(new CreateSpaceCommand
            {
                BackgroundColor = "#FFF",
                Name = "Default"
            });
            
            var command = new CreateNoteCommand
            {
                SpaceId = createSpaceResponse,
                Title = "Test Note",
                Content = "Test Content"
            };
            var commandResponse = await Send(command);

            return commandResponse;
        }
    }
}