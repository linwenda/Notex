using System.Threading;
using System.Threading.Tasks;
using Funzone.BuildingBlocks.Application.Commands;
using Funzone.BuildingBlocks.Application.Exceptions;
using Funzone.Services.Albums.Domain.Albums;
using Funzone.Services.Albums.Domain.Users;
using MediatR;

namespace Funzone.Services.Albums.Application.Commands.AddPicture
{
    public class AddPictureCommandHandler : ICommandHandler<AddPictureCommand>
    {
        private readonly IUserContext _userContext;
        private readonly IAlbumRepository _albumRepository;
        private readonly IAlbumCounter _albumCounter;

        public AddPictureCommandHandler(
            IUserContext userContext,
            IAlbumRepository albumRepository, 
            IAlbumCounter albumCounter)
        {
            _userContext = userContext;
            _albumRepository = albumRepository;
            _albumCounter = albumCounter;
        }
        
        public async Task<Unit> Handle(AddPictureCommand request, CancellationToken cancellationToken)
        {
            var album = await _albumRepository.GetByIdAsync(new AlbumId(request.AlbumId));
            if (album == null) throw new NotFoundException(nameof(Album), request.AlbumId);

            album.AddPicture(
                _albumCounter, 
                _userContext.UserId,
                request.Title, 
                request.Link, 
                request.ThumbnailLink,
                request.Description);
            
            return Unit.Value;
        }
    }
}