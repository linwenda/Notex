using System.Threading;
using System.Threading.Tasks;
using Funzone.BuildingBlocks.Application.Commands;
using Funzone.BuildingBlocks.Application.Exceptions;
using Funzone.Services.Albums.Domain.Albums;
using Funzone.Services.Albums.Domain.SharedKernel;
using MediatR;

namespace Funzone.Services.Albums.Application.Commands.ChangeVisibility
{
    public class MakePublicCommandHandler : ICommandHandler<MakePublicCommand>
    {
        private readonly IAlbumRepository _albumRepository;

        public MakePublicCommandHandler(IAlbumRepository albumRepository)
        {
            _albumRepository = albumRepository;
        }
        
        public async Task<Unit> Handle(MakePublicCommand request, CancellationToken cancellationToken)
        {
            var album = await _albumRepository.GetByIdAsync(request.AlbumId);
            
            if (album == null) throw new NotFoundException(nameof(Album), request.AlbumId);

            if (album.Visibility != Visibility.Public)
            {
                album.ChangeVisibility();
            }
            
            return Unit.Value;
        }
    }
}