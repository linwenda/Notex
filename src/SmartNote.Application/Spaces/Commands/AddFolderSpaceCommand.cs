﻿using FluentValidation;
using SmartNote.Application.Configuration.Commands;

namespace SmartNote.Application.Spaces.Commands
{
    public class AddFolderSpaceCommand : ICommand<Guid>
    {
        public Guid SpaceId { get; }
        public string Name { get; }

        public AddFolderSpaceCommand(Guid spaceId, string name)
        {
            SpaceId = spaceId;
            Name = name;
        }
    }

    public class AddFolderSpaceCommandValidator : AbstractValidator<AddFolderSpaceCommand>
    {
        public AddFolderSpaceCommandValidator()
        {
            RuleFor(v => v.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}