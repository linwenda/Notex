using FluentValidation;

namespace Funzone.Services.Albums.Application.Commands.AddPictureComment
{
    public class AddPictureCommentCommandValidator : AbstractValidator<AddPictureCommentCommand>
    {
        public AddPictureCommentCommandValidator()
        {
            RuleFor(v => v.Comment).NotNull().NotEmpty().MaximumLength(1024);
        }
    }
}