using System;
using System.Threading.Tasks;
using SmartNote.Application.Notes.Commands;
using SmartNote.Application.Spaces.Commands;
using SmartNote.Domain.Spaces;

namespace SmartNote.IntegrationTests.Notes
{
    using static TestFixture;
    
    public static class NoteTestUtil
    {
        public static async Task<Guid> CreatePublishedNote()
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

            await Send(new PublishNoteCommand(commandResponse));

            return commandResponse;
        }
    }
}