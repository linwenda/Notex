using Funzone.BuildingBlocks.Infrastructure.EventBus;
using Funzone.PhotoAlbums.Application.IntegrationEvents.Events;
using Serilog;
using System.Threading.Tasks;

namespace Funzone.PhotoAlbums.Application.IntegrationEvents.EventHandling
{
    public class UserRegisteredIntegrationEventHandler : IIntegrationEventHandler<UserRegisteredIntegrationEvent>
    {
        private readonly ILogger _logger;

        public UserRegisteredIntegrationEventHandler(ILogger logger)
        {
            _logger = logger;
        }

        public Task Handle(UserRegisteredIntegrationEvent @event)
        {
            _logger.Information("Handling UserRegisteredIntegrationEvent");
            return Task.CompletedTask;
        }
    }
}