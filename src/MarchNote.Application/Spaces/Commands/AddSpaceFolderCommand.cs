using System;
using FluentValidation;
using MarchNote.Application.Configuration.Commands;
using MarchNote.Application.Configuration.Responses;

namespace MarchNote.Application.Spaces.Commands
{
    public class AddSpaceFolderCommand : ICommand<MarchNoteResponse<Guid>>
    {
        public Guid SpaceId { get; }
        public string Name { get; }

        public AddSpaceFolderCommand(Guid spaceId, string name)
        {
            SpaceId = spaceId;
            Name = name;
        }
    }

    public class AddSpaceFolderCommandValidator : AbstractValidator<AddSpaceFolderCommand>
    {
        public AddSpaceFolderCommandValidator()
        {
            RuleFor(v => v.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}