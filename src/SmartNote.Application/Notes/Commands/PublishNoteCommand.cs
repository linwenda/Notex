using MediatR;
using SmartNote.Application.Configuration.Commands;

namespace SmartNote.Application.Notes.Commands
{
    public class PublishNoteCommand : ICommand<Unit>
    {
        public Guid NoteId { get; }

        public PublishNoteCommand(Guid noteId)
        {
            NoteId = noteId;
        }
    }
}