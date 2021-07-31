using System;
using FluentValidation;
using MarchNote.Application.Configuration.Commands;
using MarchNote.Application.Configuration.Responses;

namespace MarchNote.Application.Spaces.Commands
{
    public class AddSubSpaceFolderCommand : ICommand<MarchNoteResponse<Guid>>
    {
        public Guid ParentSpaceFolderId { get; }
        public string Name { get; }

        public AddSubSpaceFolderCommand(Guid parentSpaceFolderId, string name)
        {
            ParentSpaceFolderId = parentSpaceFolderId;
            Name = name;
        }
    }

    public class AddSubSpaceFolderCommandValidator : AbstractValidator<AddSubSpaceFolderCommand>
    {
        public AddSubSpaceFolderCommandValidator()
        {
            RuleFor(v => v.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}