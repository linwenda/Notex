using SmartNote.Application.Configuration.Commands;

namespace SmartNote.Application.Notes.Commands
{
    public class ForkNoteCommand : ICommand<Guid>
    {
        public Guid NoteId { get; }

        public ForkNoteCommand(Guid noteId)
        {
            NoteId = noteId;
        }
    }
}