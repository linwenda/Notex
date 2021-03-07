using Autofac;
using Funzone.BuildingBlocks.EventBus.MassTransit;
using Funzone.IdentityAccess.Infrastructure.DataAccess;
using Funzone.IdentityAccess.Infrastructure.Domain;
using Funzone.IdentityAccess.Infrastructure.EventBus;
using Funzone.IdentityAccess.Infrastructure.Mediator;
using Microsoft.Extensions.Logging;
using Serilog.Extensions.Logging;
using ILogger = Serilog.ILogger;

namespace Funzone.IdentityAccess.Infrastructure
{
    public class IdentityAccessStartup
    {
        public static void Initialize(
            string connectionString,
            ILogger logger)
        {
            var containerBuilder = new ContainerBuilder();

            var loggerFactory = new LoggerFactory(new[]
            {
                new SerilogLoggerProvider(logger)
            });

            containerBuilder.RegisterModule(new DataAccessModule(connectionString, loggerFactory));
            containerBuilder.RegisterModule(new DomainModule());
            containerBuilder.RegisterModule(new MediatorModule());
            // containerBuilder.RegisterModule(new EventBusModule());

            //EventBusStartup.Initialize(logger,
            //    new MassTransitEventBusSettings(
            //        "rabbitmq://localhost",
            //        "funzone",
            //        "funzone",
            //        "identity_access"));

            var container = containerBuilder.Build();
            IdentityAccessCompositionRoot.SetContainer(container);
        }
    }
}