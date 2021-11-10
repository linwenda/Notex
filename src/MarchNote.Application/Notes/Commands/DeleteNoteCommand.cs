using System;
using MarchNote.Application.Configuration.Commands;
using MediatR;

namespace MarchNote.Application.Notes.Commands
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