using System.Threading;
using System.Threading.Tasks;
using Funzone.BuildingBlocks.Application.Commands;
using Funzone.BuildingBlocks.Application.Exceptions;
using Funzone.Services.Albums.Domain.Albums;
using Funzone.Services.Albums.Domain.SharedKernel;
using Funzone.Services.Albums.Domain.Users;
using MediatR;

namespace Funzone.Services.Albums.Application.Commands.ChangeVisibility
{
    public class ChangeVisibilityCommandHandler : ICommandHandler<ChangeVisibilityCommand>
    {
        private readonly IUserContext _userContext;
        private readonly IAlbumRepository _albumRepository;

        public ChangeVisibilityCommandHandler(IUserContext userContext, IAlbumRepository albumRepository)
        {
            _userContext = userContext;
            _albumRepository = albumRepository;
        }
        
        public async Task<Unit> Handle(ChangeVisibilityCommand request, CancellationToken cancellationToken)
        {
            var album = await _albumRepository.GetByIdAsync(new AlbumId(request.AlbumId));
            
            if (album == null) throw new NotFoundException(nameof(Album), request.AlbumId);

            album.ChangeVisibility(_userContext.UserId, Visibility.Of(request.Visibility));
            
            return Unit.Value;
        }
    }
}