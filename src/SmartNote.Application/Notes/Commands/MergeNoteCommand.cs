using MediatR;
using SmartNote.Application.Configuration.Commands;

namespace SmartNote.Application.Notes.Commands
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