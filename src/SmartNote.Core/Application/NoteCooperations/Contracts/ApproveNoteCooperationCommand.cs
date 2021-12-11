using MediatR;

namespace SmartNote.Core.Application.NoteCooperations.Contracts
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