using System;
using System.Threading.Tasks;
using MarchNote.Application.Notes.Commands;

namespace MarchNote.IntegrationTests.Notes
{
    using static TestFixture;
    
    public static class NoteTestUtil
    {
        public static async Task<Guid> CreatePublishedNote()
        {
            var command = new CreateNoteCommand
            {
                Title = "Test Note",
                Content = "Test Content"
            };
            var commandResponse = await Send(command);

            await Send(new PublishNoteCommand(commandResponse.Data));

            return commandResponse.Data;
        }
    }
}