using Funzone.BuildingBlocks.Application.Commands;
using Funzone.Services.Albums.Domain.PhotoAlbums;
using Funzone.Services.Albums.Domain.Users;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Funzone.Services.Albums.Application.Commands.CreateAlbum
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