using System;
using System.Threading.Tasks;
using MarchNote.Application.Notes.Commands;
using MarchNote.Application.Spaces.Commands;

namespace MarchNote.IntegrationTests.Notes
{
    using static TestFixture;
    
    public static class NoteTestUtil
    {
        public static async Task<Guid> CreatePublishedNote()
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

            await Send(new PublishNoteCommand(commandResponse.Data));

            return commandResponse.Data;
        }
    }
}