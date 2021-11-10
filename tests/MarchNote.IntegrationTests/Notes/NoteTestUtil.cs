using System;
using System.Threading.Tasks;
using MarchNote.Application.Notes.Commands;
using MarchNote.Application.Spaces.Commands;
using MarchNote.Domain.Shared;

namespace MarchNote.IntegrationTests.Notes
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
                Content = "Test Content"
            };
            var commandResponse = await Send(command);

            await Send(new PublishNoteCommand(commandResponse));

            return commandResponse;
        }
    }
}