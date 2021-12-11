using MediatR;

namespace SmartNote.Core.Application.Notes.Contracts
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