using System;
using FluentValidation;
using MarchNote.Application.Configuration.Commands;

namespace MarchNote.Application.NoteCooperations.Commands
{
    public class ApplyForNoteCooperationCommand : ICommand<Guid>
    {
        public ApplyForNoteCooperationCommand(Guid noteId,string comment)
        {
            NoteId = noteId;
            Comment = comment;
        }
        
        public Guid NoteId { get; }
        public string Comment { get; }
    }

    public class ApplyForNoteCooperationCommandValidator : AbstractValidator<ApplyForNoteCooperationCommand>
    {
        public ApplyForNoteCooperationCommandValidator()
        {
            RuleFor(v => v.Comment).NotNull().NotEmpty().MaximumLength(256);
        }
    }
}