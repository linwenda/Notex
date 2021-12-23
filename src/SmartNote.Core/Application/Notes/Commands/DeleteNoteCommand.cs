using MediatR;

namespace SmartNote.Core.Application.Notes.Commands
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