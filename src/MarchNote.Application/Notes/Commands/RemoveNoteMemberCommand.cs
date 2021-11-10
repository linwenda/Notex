using System;
using MarchNote.Application.Configuration.Commands;
using MediatR;

namespace MarchNote.Application.Notes.Commands
{
    public class RemoveNoteMemberCommand : ICommand<Unit>
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