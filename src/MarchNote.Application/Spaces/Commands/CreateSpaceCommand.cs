using System;
using FluentValidation;
using MarchNote.Application.Configuration.Commands;
using MarchNote.Domain.Shared;

namespace MarchNote.Application.Spaces.Commands
{
    public class CreateSpaceCommand : ICommand<Guid>
    {
        public string Name { get; set; }
        public string BackgroundColor { get; set; }
        public Guid? BackgroundImageId { get; set; }
        public Visibility Visibility { get; set; }
        public string Description { get; set; }
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