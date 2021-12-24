using FluentValidation;
using SmartNote.Application.Configuration.Commands;
using SmartNote.Domain.Spaces;

namespace SmartNote.Application.Spaces.Commands
{
    public class CreateSpaceCommand : ICommand<Guid>
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