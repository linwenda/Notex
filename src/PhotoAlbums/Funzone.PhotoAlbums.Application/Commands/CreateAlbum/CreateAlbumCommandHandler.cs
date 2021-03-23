using Funzone.BuildingBlocks.Application.Commands;
using Funzone.BuildingBlocks.Infrastructure;
using Funzone.PhotoAlbums.Domain.Albums;
using Funzone.PhotoAlbums.Domain.Users;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Funzone.PhotoAlbums.Application.Commands.CreateAlbum
{
    public class CreateAlbumCommandHandler : ICommandHandler<CreateAlbumCommand>
    {
        private readonly IUserContext _userContext;
        private readonly IAlbumCounter _albumCounter;
        private readonly IAlbumRepository _albumRepository;

        public CreateAlbumCommandHandler(
            IUserContext userContext,
            IAlbumCounter albumCounter,
            IAlbumRepository albumRepository)
        {
            _userContext = userContext;
            _albumCounter = albumCounter;
            _albumRepository = albumRepository;
        }

        public async Task<Unit> Handle(CreateAlbumCommand request, CancellationToken cancellationToken)
        {
            var album = Album.Create(
                request.Name,
                _userContext.UserId,
                _albumCounter);

            await _albumRepository.AddAsync(album);
            return Unit.Value;
        }
    }
}