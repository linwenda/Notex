using Funzone.IdentityAccess.IntegrationEvents;
using MassTransit;
using Serilog;
using System.Threading.Tasks;

namespace Funzone.PhotoAlbums.Application.IntegrationEventsHandling
{
    public class UserRegisteredIntegrationEventHandler : IConsumer<UserRegisteredIntegrationEvent>
    {
        private readonly ILogger _logger;

        public UserRegisteredIntegrationEventHandler(ILogger logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<UserRegisteredIntegrationEvent> context)
        {
            _logger.Information(
                "Handling integration event: {IntegrationEventId} at {Context} - ({@IntegrationEvent})",
                context.Message.Id,
                "PhotoAlbums",
                context.Message);

            return Task.CompletedTask;
        }
    }
}