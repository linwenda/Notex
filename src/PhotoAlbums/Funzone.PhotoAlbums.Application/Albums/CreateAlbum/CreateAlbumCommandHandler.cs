using Funzone.BuildingBlocks.Application.Commands;
using Funzone.PhotoAlbums.Domain.Albums;
using Funzone.PhotoAlbums.Domain.Users;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Funzone.PhotoAlbums.Application.Albums.CreateAlbum
{
    public class CreateAlbumCommandHandler : ICommandHandler<CreateAlbumCommand>
    {
        private readonly IUserContext _userContext;
        private readonly IAlbumCounter _albumCounter;

        public CreateAlbumCommandHandler(
            IUserContext userContext,
            IAlbumCounter albumCounter)
        {
            _userContext = userContext;
            _albumCounter = albumCounter;
        }

        public Task<Unit> Handle(CreateAlbumCommand request, CancellationToken cancellationToken)
        {
            Album.Create(
                request.Name,
                _userContext.UserId,
                _albumCounter);

            return Task.FromResult(Unit.Value);
        }
    }
}