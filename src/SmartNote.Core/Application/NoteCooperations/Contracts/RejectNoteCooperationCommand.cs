using MediatR;

namespace SmartNote.Core.Application.NoteCooperations.Contracts
{
    public class RejectNoteCooperationCommand : ICommand<Unit>
    {
        public Guid CooperationId { get; }
        public string RejectReason { get; }

        public RejectNoteCooperationCommand(Guid cooperationId, string rejectReason)
        {
            CooperationId = cooperationId;
            RejectReason = rejectReason;
        }
    }
}