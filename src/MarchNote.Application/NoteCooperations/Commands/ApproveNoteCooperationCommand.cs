using System;
using MarchNote.Application.Configuration.Commands;
using MarchNote.Application.Configuration.Responses;

namespace MarchNote.Application.NoteCooperations.Commands
{
    public class ApproveNoteCooperationCommand : ICommand<MarchNoteResponse>
    {
        public Guid CooperationId { get; }

        public ApproveNoteCooperationCommand(Guid cooperationId)
        {
            CooperationId = cooperationId;
        }
    }
}