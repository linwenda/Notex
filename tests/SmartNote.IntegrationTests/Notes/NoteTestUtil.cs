using System;
using System.Threading.Tasks;
using SmartNote.Core.Application.Notes.Contracts;
using SmartNote.Core.Application.Spaces.Contracts;
using SmartNote.Core.Domain.Spaces;

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
                Content = "Test Content"
            };
            var commandResponse = await Send(command);

            await Send(new PublishNoteCommand(commandResponse));

            return commandResponse;
        }
    }
}