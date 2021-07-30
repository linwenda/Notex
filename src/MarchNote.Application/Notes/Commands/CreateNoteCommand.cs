using System;
using FluentValidation;
using MarchNote.Application.Configuration.Commands;
using MarchNote.Application.Configuration.Responses;

namespace MarchNote.Application.Notes.Commands
{
    public class CreateNoteCommand : ICommand<MarchNoteResponse<Guid>>
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }

    public class CreateNoteCommandValidator : AbstractValidator<CreateNoteCommand>
    {
        public CreateNoteCommandValidator()
        {
            RuleFor(v => v.Title)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(v => v.Content)
                .NotNull();
        }
    }
}