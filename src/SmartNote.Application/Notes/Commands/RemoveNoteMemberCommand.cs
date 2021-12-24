using MediatR;
using SmartNote.Application.Configuration.Commands;

namespace SmartNote.Application.Notes.Commands
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