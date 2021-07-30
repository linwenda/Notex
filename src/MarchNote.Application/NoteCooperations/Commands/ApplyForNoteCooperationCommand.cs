using System;
using FluentValidation;
using MarchNote.Application.Configuration.Commands;
using MarchNote.Application.Configuration.Responses;

namespace MarchNote.Application.NoteCooperations.Commands
{
    public class ApplyForNoteCooperationCommand : ICommand<MarchNoteResponse<Guid>>
    {
        public Guid NoteId { get; set; }
        public string Comment { get; set; }
    }

    public class ApplyForNoteCooperationCommandValidator : AbstractValidator<ApplyForNoteCooperationCommand>
    {
        public ApplyForNoteCooperationCommandValidator()
        {
            RuleFor(v => v.Comment).NotNull().NotEmpty().MaximumLength(256);
        }
    }
}