using System;
using System.Threading;
using System.Threading.Tasks;
using Funzone.BuildingBlocks.Application.Commands;
using Funzone.BuildingBlocks.Application.Exceptions;
using Funzone.Services.Albums.Domain.Albums;
using Funzone.Services.Albums.Domain.Pictures;
using Funzone.Services.Albums.Domain.Users;
using MediatR;

namespace Funzone.Services.Albums.Application.Commands.AddPicture
{
    public class AddPictureCommandHandler : ICommandHandler<AddPictureCommand>
    {
        private readonly IUserContext _userContext;
        private readonly IAlbumRepository _albumRepository;
        private readonly IPictureRepository _pictureRepository;
        private readonly IPictureCounter _pictureCounter;

        public AddPictureCommandHandler(
            IUserContext userContext,
            IAlbumRepository albumRepository,
            IPictureRepository pictureRepository,
            IPictureCounter pictureCounter)
        {
            _userContext = userContext;
            _albumRepository = albumRepository;
            _pictureRepository = pictureRepository;
            _pictureCounter = pictureCounter;
        }
        
        public async Task<Unit> Handle(AddPictureCommand request, CancellationToken cancellationToken)
        {
            var album = await _albumRepository.GetByIdAsync(new AlbumId(request.AlbumId));
            if (album == null) throw new NotFoundException(nameof(Album), request.AlbumId);

            if (album.UserId != _userContext.UserId)
                throw new UnauthorizedAccessException("The album not belong to you");
            
            await _pictureRepository.AddAsync(Picture.Add(
                _pictureCounter,
                album.Id,
                _userContext.UserId, 
                request.Title,
                request.Link,
                request.ThumbnailLink,
                request.Description));
            
            return Unit.Value;
        }
    }
}