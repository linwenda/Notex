using System;
using MarchNote.Application.Configuration.Commands;

namespace MarchNote.Application.Notes.Commands
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