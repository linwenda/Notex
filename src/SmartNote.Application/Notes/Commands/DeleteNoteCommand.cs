using MediatR;
using SmartNote.Application.Configuration.Commands;

namespace SmartNote.Application.Notes.Commands
{
    public class DeleteNoteCommand : ICommand<Unit>
    {
        public Guid NoteId { get; }

        public DeleteNoteCommand(Guid noteId)
        {
            NoteId = noteId;
        }
    }
}