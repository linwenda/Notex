using System;
using MarchNote.Application.Configuration.Commands;
using MediatR;

namespace MarchNote.Application.NoteCooperations.Commands
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