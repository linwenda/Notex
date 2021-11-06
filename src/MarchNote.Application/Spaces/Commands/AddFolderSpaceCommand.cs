﻿using System;
using FluentValidation;
using MarchNote.Application.Configuration.Commands;
using MarchNote.Application.Configuration.Responses;

namespace MarchNote.Application.Spaces.Commands
{
    public class AddFolderSpaceCommand : ICommand<MarchNoteResponse<Guid>>
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