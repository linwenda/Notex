using FluentValidation;

namespace Funzone.PhotoAlbums.Application.Commands.CreateAlbum
{
    public class CreateAlbumCommandValidator : AbstractValidator<CreateAlbumCommand>
    {
        public CreateAlbumCommandValidator()
        {
            RuleFor(v => v.Name).NotNull().Length(1, 50);
        }
    }
}