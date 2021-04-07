using FluentValidation;

namespace Funzone.Services.Albums.Application.Commands.AddPicture
{
    public class AddPictureCommandValidator : AbstractValidator<AddPictureCommand>
    {
        public AddPictureCommandValidator()
        {
            RuleFor(v => v.Title)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(v => v.Link)
                .NotNull()
                .NotEmpty()
                .MaximumLength(512);

            RuleFor(v => v.ThumbnailLink)
                .MaximumLength(512);

            RuleFor(v => v.Description)
                .MaximumLength(512);
        }
    }
}