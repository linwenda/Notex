using System;
using MarchNote.Application.Configuration.Commands;
using MarchNote.Application.Configuration.Responses;

namespace MarchNote.Application.Notes.Commands
{
    public class DraftOutNoteCommand : ICommand<MarchNoteResponse<Guid>>
    {
        public Guid NoteId { get; }

        public DraftOutNoteCommand(Guid noteId)
        {
            NoteId = noteId;
        }
    }
}