﻿using MediatR;
using SmartNote.Application.Configuration.Commands;

namespace SmartNote.Application.NoteCooperations.Commands
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