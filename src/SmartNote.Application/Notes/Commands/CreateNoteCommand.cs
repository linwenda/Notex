using FluentValidation;
using SmartNote.Application.Configuration.Commands;

namespace SmartNote.Application.Notes.Commands
{
    public class CreateNoteCommand : ICommand<Guid>
    {
        public Guid SpaceId { get; set; }
        public string Title { get; set; }
    }

    public class CreateNoteCommandValidator : AbstractValidator<CreateNoteCommand>
    {
        public CreateNoteCommandValidator()
        {
            RuleFor(v => v.Title)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}