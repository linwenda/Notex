using System.Threading;
using System.Threading.Tasks;
using Funzone.BuildingBlocks.Infrastructure;
using Funzone.BuildingBlocks.Infrastructure.EventBus;
using Funzone.Services.Albums.Application.IntegrationEvents.Events;
using Funzone.Services.Albums.Domain.PhotoAlbums;
using Funzone.Services.Albums.Domain.Users;
using Serilog;

namespace Funzone.Services.Albums.Application.IntegrationEvents.EventHandling
{
    public class UserRegisteredIntegrationEventHandler : IIntegrationEventHandler<UserRegisteredIntegrationEvent>
    {
        private readonly ILogger _logger;
        private readonly IAlbumCounter _albumCounter;
        private readonly IAlbumRepository _albumRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserRegisteredIntegrationEventHandler(
            ILogger logger,
            IAlbumCounter albumCounter,
            IAlbumRepository albumRepository,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _albumCounter = albumCounter;
            _albumRepository = albumRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UserRegisteredIntegrationEvent @event)
        {
            _logger.Information(
                "----- Handling integration event: {IntegrationEventId} at PhotoAlbum.Api - ({@IntegrationEvent})",
                @event.Id, @event);
            
            var album = Album.Create("Default", new UserId(@event.UserId), _albumCounter);
            await _albumRepository.AddAsync(album);
            await _unitOfWork.CommitAsync(CancellationToken.None);
        }
    }
}