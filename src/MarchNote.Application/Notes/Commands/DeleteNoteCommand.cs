using System;
using MarchNote.Application.Configuration.Commands;
using MarchNote.Application.Configuration.Responses;

namespace MarchNote.Application.Notes.Commands
{
    public class DeleteNoteCommand : ICommand<MarchNoteResponse>
    {
        public Guid NoteId { get; }

        public DeleteNoteCommand(Guid noteId)
        {
            NoteId = noteId;
        }
    }
}