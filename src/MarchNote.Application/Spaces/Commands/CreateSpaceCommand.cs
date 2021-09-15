using System;
using FluentValidation;
using MarchNote.Application.Configuration.Commands;
using MarchNote.Application.Configuration.Responses;
using MarchNote.Domain.Shared;

namespace MarchNote.Application.Spaces.Commands
{
    public class CreateSpaceCommand : ICommand<MarchNoteResponse<Guid>>
    {
        public string Name { get; set; }
        public string BackgroundColor { get; set; }
        public Guid? BackgroundImageId { get; set; }
        public Visibility Visibility { get; set; }
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