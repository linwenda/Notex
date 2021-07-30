using System;
using MarchNote.Application.Configuration.Commands;
using MarchNote.Application.Configuration.Responses;

namespace MarchNote.Application.Notes.Commands
{
    public class RemoveNoteMemberCommand : ICommand<MarchNoteResponse>
    {
        public Guid NoteId { get; }
        public Guid UserId { get; }

        public RemoveNoteMemberCommand(Guid noteId, Guid userId)
        {
            NoteId = noteId;
            UserId = userId;
        }
    }
}