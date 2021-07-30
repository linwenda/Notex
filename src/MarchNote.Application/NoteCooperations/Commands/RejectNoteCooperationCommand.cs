using System;
using MarchNote.Application.Configuration.Commands;
using MarchNote.Application.Configuration.Responses;

namespace MarchNote.Application.NoteCooperations.Commands
{
    public class RejectNoteCooperationCommand : ICommand<MarchNoteResponse>
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