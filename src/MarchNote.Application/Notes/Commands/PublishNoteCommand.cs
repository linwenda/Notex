using System;
using MarchNote.Application.Configuration.Commands;
using MediatR;

namespace MarchNote.Application.Notes.Commands
{
    public class PublishNoteCommand : ICommand<Unit>
    {
        public Guid NoteId { get; }

        public PublishNoteCommand(Guid noteId)
        {
            NoteId = noteId;
        }
    }
}