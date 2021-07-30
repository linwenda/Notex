using System;
using MarchNote.Application.Configuration.Commands;
using MarchNote.Application.Configuration.Responses;

namespace MarchNote.Application.Notes.Commands
{
    public class MergeNoteCommand : ICommand<MarchNoteResponse>
    {
        public Guid NoteId { get; }

        public MergeNoteCommand(Guid noteId)
        {
            NoteId = noteId;
        }
    }
}