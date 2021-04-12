using FluentValidation;

namespace Funzone.Services.Albums.Application.Commands.EditPictureComment
{
    public class EditPictureCommentCommandValidator : AbstractValidator<EditPictureCommentCommand>
    {
        public EditPictureCommentCommandValidator()
        {
            RuleFor(v => v.Comment).NotNull().NotEmpty().MaximumLength(1024);
        }
    }
}