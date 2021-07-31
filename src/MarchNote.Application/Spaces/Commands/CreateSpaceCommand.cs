using System;
using FluentValidation;
using MarchNote.Application.Configuration.Commands;
using MarchNote.Application.Configuration.Responses;

namespace MarchNote.Application.Spaces.Commands
{
    public class CreateSpaceCommand : ICommand<MarchNoteResponse<Guid>>
    {
        public string Name { get; set; }
        public string Color { get; set; } = "#FFFFFF";
        public string Icon { get; set; }
    }

    public class CreateSpaceCommandValidator : AbstractValidator<CreateSpaceCommand>
    {
        public CreateSpaceCommandValidator()
        {
            RuleFor(v => v.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}