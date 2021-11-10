using System;
using FluentValidation;
using MarchNote.Application.Configuration.Commands;
using MediatR;

namespace MarchNote.Application.Spaces.Commands
{
    public class RenameSpaceCommand : ICommand<Unit>
    {
        public Guid SpaceId { get; }
        public string Name { get; }

        public RenameSpaceCommand(Guid spaceId,string name)
        {
            SpaceId = spaceId;
            Name = name;
        }
    }

    public class RenameSpaceCommandValidator : AbstractValidator<RenameSpaceCommand>
    {
        public RenameSpaceCommandValidator()
        {
            RuleFor(v => v.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}