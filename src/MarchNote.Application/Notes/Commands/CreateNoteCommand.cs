using System;
using System.Collections.Generic;
using FluentValidation;
using MarchNote.Application.Configuration.Commands;

namespace MarchNote.Application.Notes.Commands
{
    public class CreateNoteCommand : ICommand<Guid>
    {
        public Guid SpaceId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public List<string> Tags { get; set; }
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