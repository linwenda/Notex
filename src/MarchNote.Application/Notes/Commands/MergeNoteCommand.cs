using System;
using MarchNote.Application.Configuration.Commands;
using MediatR;

namespace MarchNote.Application.Notes.Commands
{
    public class MergeNoteCommand : ICommand<Unit>
    {
        public Guid NoteId { get; }

        public MergeNoteCommand(Guid noteId)
        {
            NoteId = noteId;
        }
    }
}