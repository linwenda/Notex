using Funzone.BuildingBlocks.EventBus.MassTransit;
using MassTransit;
using Serilog;

namespace Funzone.IdentityAccess.Infrastructure.EventBus
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
            });

            bus.Start();
        }
    }
}