using System;
using MarchNote.Application.Configuration.Commands;

namespace MarchNote.Application.Notes.Commands
{
    public class DraftOutNoteCommand : ICommand<Guid>
    {
        public Guid NoteId { get; }

        public DraftOutNoteCommand(Guid noteId)
        {
            NoteId = noteId;
        }
    }
}