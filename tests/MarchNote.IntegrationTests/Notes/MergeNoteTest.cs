using System.Threading.Tasks;
using MarchNote.Application.Notes.Commands;
using MarchNote.Application.Notes.Queries;
using MarchNote.Application.Spaces.Commands;
using MarchNote.Domain.Notes;
using MarchNote.Domain.Shared;
using MarchNote.Domain.Spaces;
using NUnit.Framework;
using Shouldly;
using TestStack.BDDfy;

namespace MarchNote.IntegrationTests.Notes
{
    using static TestFixture;

    public class MergeNoteTest : TestBase
    {
        private NoteId _noteId;
        private NoteId _draftOutNoteId;
        private EditNoteCommand _editNoteCommand;

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
                SpaceId = spaceResponse.Data,
                Title = ".NET 5",
                Content = ".NET 5.0 is the next major release of .NET Core following 3.1."
            });

            _noteId = new NoteId(response.Data);
        }

        private async Task AndGivenTheNoteIsPublished()
        {
            await Send(new PublishNoteCommand(_noteId.Value));
        }

        private async Task AndGivenTheDraftFromNote()
        {
            var response = await Send(new DraftOutNoteCommand(_noteId.Value));

            _draftOutNoteId = new NoteId(response.Data);
        }

        private async Task WhenTheDraftWasEdited()
        {
            _editNoteCommand = new EditNoteCommand(_draftOutNoteId.Value, "test", "test content");
            await Send(_editNoteCommand);
        }

        private async Task ThenTheDraftMergeToNote()
        {
            await Send(new MergeNoteCommand(_draftOutNoteId.Value));
        }

        private async Task AndTheNoteWasUpdated()
        {
            var response = await Send(new GetNoteQuery(_noteId.Value));
            response.Data.Title.ShouldBe(_editNoteCommand.Title);
            response.Data.Content.ShouldBe(_editNoteCommand.Content);
            response.Data.Version.ShouldBe(2);
            response.Data.Status.ShouldBe(NoteStatus.Published);
        }

        private async Task AndTheDraftWasDeleted()
        {
            var response = await Send(new GetNoteQuery(_draftOutNoteId.Value));
            response.Data.ShouldBeNull();
        }

        [Test]
        public void Execute()
        {
            this.BDDfy();
        }
    }
}