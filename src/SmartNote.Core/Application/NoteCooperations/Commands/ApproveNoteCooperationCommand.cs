using MediatR;

namespace SmartNote.Core.Application.NoteCooperations.Commands
{
    public class ApproveNoteCooperationCommand : ICommand<Unit>
    {
        public Guid CooperationId { get; }

        public ApproveNoteCooperationCommand(Guid cooperationId)
        {
            CooperationId = cooperationId;
        }
    }
}