using FluentValidation;

namespace Funzone.Services.Albums.Application.Commands.CreateAlbum
{
    public class CreateAlbumCommandValidator : AbstractValidator<CreateAlbumCommand>
    {
        public CreateAlbumCommandValidator()
        {
            RuleFor(v => v.Name).NotNull().Length(1, 50);
        }
    }
}