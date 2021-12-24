using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using Shouldly;
using SmartNote.Application.Notes.Commands;
using SmartNote.Application.Notes.Queries;
using SmartNote.Application.Spaces.Commands;
using SmartNote.Domain.Notes;
using SmartNote.Domain.Notes.Blocks;
using SmartNote.Domain.Spaces;
using TestStack.BDDfy;

namespace SmartNote.IntegrationTests.Notes
{
    using static TestFixture;

    [Ignore("test")]
    public class MergeNoteTest : TestBase
    {
        private NoteId _noteId;
        private NoteId _draftOutNoteId;
        private UpdateNoteCommand _updateNoteCommand;

        private async Task GivenNewNote()
        {
            var spaceResponse = await Send(new CreateSpaceCommand
            {
                BackgroundColor = "#FFF",
                Name = "Default",
                Visibility = Visibility.Public
            });

            var response = await Send(new CreateNoteCommand
            {
                SpaceId = spaceResponse,
                Title = ".NET 5",
            });

            _noteId = new NoteId(response);
        }

        private async Task AndGivenTheNoteIsPublished()
        {
            await Send(new PublishNoteCommand(_noteId.Value));
        }

        private async Task AndGivenTheDraftFromNote()
        {
            var response = await Send(new ForkNoteCommand(_noteId.Value));

            _draftOutNoteId = new NoteId(response);
        }

        private async Task WhenTheDraftWasEdited()
        {
            _updateNoteCommand = new UpdateNoteCommand(_draftOutNoteId.Value, "test", new List<BlockDto>
            {
                new BlockDto
                {
                    Id = "id",
                    Type = BlockType.Paragraph.Value,
                    Data = new Paragraph("test"),
                }
            });
            await Send(_updateNoteCommand);
        }

        private Task ThenTheDraftMergeToNote()
        {
            return Task.CompletedTask;
        }

        private async Task AndTheNoteWasUpdated()
        {
            var response = await Send(new GetNoteQuery(_noteId.Value));
            response.Title.ShouldBe(_updateNoteCommand.Title);
            // response.Blocks.ShouldBe(_updateNoteCommand.Blocks);
            response.Version.ShouldBe(2);
            response.Status.ShouldBe(NoteStatus.Published);
        }

        private async Task AndTheDraftWasDeleted()
        {
            var response = await Send(new GetNoteQuery(_draftOutNoteId.Value));
            response.ShouldBeNull();
        }

        [Test]
        public void Execute()
        {
            this.BDDfy();
        }
    }
}