namespace SmartNote.Core.Application.Notes.Contracts
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