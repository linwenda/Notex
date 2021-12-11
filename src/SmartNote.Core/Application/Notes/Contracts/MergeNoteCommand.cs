using MediatR;

namespace SmartNote.Core.Application.Notes.Contracts
{
    public class MergeNoteCommand : ICommand<Unit>
    {
        public Guid NoteId { get; }
        public Guid ForkId { get; }

        public MergeNoteCommand(Guid noteId, Guid forkId)
        {
            NoteId = noteId;
            ForkId = forkId;
        }
    }
}