using System;
using MarchNote.Application.Configuration.Commands;
using MediatR;

namespace MarchNote.Application.NoteCooperations.Commands
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