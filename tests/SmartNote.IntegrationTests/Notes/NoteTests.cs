using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;
using Shouldly;
using SmartNote.Application.Notes.Commands;
using SmartNote.Application.Notes.Queries;
using SmartNote.Application.Spaces.Commands;
using SmartNote.Domain.Notes;
using SmartNote.Domain.Notes.Blocks;
using SmartNote.Domain.Spaces;

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
            };
            var commandResponse = await Send(command);

            var queryResponse = await Send(new GetNoteQuery(commandResponse));
            queryResponse.ShouldNotBeNull();
            queryResponse.Title.ShouldBe(command.Title);
            queryResponse.AuthorId.ShouldBe(CurrentUser);
            queryResponse.Status.ShouldBe(NoteStatus.Draft);
        }

        [Test]
        public async Task ShouldUpdateNote()
        {
            var noteId = await CreateTestNote();

            var editCommand = new UpdateNoteCommand(noteId, "Title#2", new List<BlockDto>
            {
                new BlockDto
                {
                    Id = "test1",
                    Type = BlockType.Header.ToString(),
                    Data = new Header("header1", 1)
                },
                new BlockDto
                {
                    Id = "test2",
                    Type = BlockType.Header.ToString(),
                    Data = new Header("header2", 2)
                }
            });
            await Send(editCommand);

            var queryResponse = await Send(new GetNoteQuery(noteId));
            queryResponse.ShouldNotBeNull();
            queryResponse.Title.ShouldBe(editCommand.Title);
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

            var command = new UpdateNoteCommand(noteId, "Title#2", new List<BlockDto>());
            await Send(command);

            var queryResponse = await Send(new GetNoteQuery(noteId));
            queryResponse.ShouldNotBeNull();
            queryResponse.Title.ShouldBe(command.Title);
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
        public async Task ShouldForkPublishedNote()
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
            };
            var commandResponse = await Send(command);

            return commandResponse;
        }
    }
}