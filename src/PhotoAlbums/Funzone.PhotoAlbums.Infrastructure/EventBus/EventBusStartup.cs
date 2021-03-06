using Funzone.BuildingBlocks.EventBus.MassTransit;
using Funzone.PhotoAlbums.Application.IntegrationEventsHandling;
using MassTransit;
using Serilog;
using System.Threading.Tasks;

namespace Funzone.PhotoAlbums.Infrastructure.EventBus
{
    internal static class EventBusStartup
    {
        internal static void Initialize(ILogger logger, MassTransitEventBusSettings busSettings)
        {
            logger.Information("Starting MassTransit Bus");

            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                sbc.Host(busSettings.HostAddress, cfg =>
                {
                    cfg.Username(busSettings.UserName);
                    cfg.Password(busSettings.Password);
                });

                sbc.ReceiveEndpoint("photo_albums", ep =>
                {
                    ep.Handler<UserRegisteredIntegrationEventHandler>(context =>
                    {
                        logger.Information("Received: UserRegisteredIntegration");
                        return Task.CompletedTask;
                    });
                });
            });

            bus.Start();
        }
    }
}