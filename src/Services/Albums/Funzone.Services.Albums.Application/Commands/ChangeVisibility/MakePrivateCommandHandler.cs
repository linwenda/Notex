using System.Threading;
using System.Threading.Tasks;
using Funzone.BuildingBlocks.Application.Commands;
using Funzone.BuildingBlocks.Application.Exceptions;
using Funzone.Services.Albums.Domain.Albums;
using Funzone.Services.Albums.Domain.SharedKernel;
using MediatR;

namespace Funzone.Services.Albums.Application.Commands.ChangeVisibility
{
    public class MakePrivateCommandHandler : ICommandHandler<MakePrivateCommand>
    {
        private readonly IAlbumRepository _albumRepository;

        public MakePrivateCommandHandler(IAlbumRepository albumRepository)
        {
            _albumRepository = albumRepository;
        }
        
        public async Task<Unit> Handle(MakePrivateCommand request, CancellationToken cancellationToken)
        {
            var album = await _albumRepository.GetByIdAsync(new AlbumId(request.AlbumId));
            
            if (album == null) throw new NotFoundException(nameof(Album), request.AlbumId);
            
            album.MakePrivate();
            
            return Unit.Value;
        }
    }
}