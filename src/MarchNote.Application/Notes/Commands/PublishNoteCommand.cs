using System;
using MarchNote.Application.Configuration.Commands;
using MarchNote.Application.Configuration.Responses;

namespace MarchNote.Application.Notes.Commands
{
    public class PublishNoteCommand : ICommand<MarchNoteResponse>
    {
        public Guid NoteId { get; }

        public PublishNoteCommand(Guid noteId)
        {
            NoteId = noteId;
        }
    }
}