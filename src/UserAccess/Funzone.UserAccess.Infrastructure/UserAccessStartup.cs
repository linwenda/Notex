using Autofac;
using Funzone.BuildingBlocks.EventBus.MassTransit;
using Funzone.BuildingBlocks.Infrastructure.EventBus;
using Funzone.UserAccess.Infrastructure.DataAccess;
using Funzone.UserAccess.Infrastructure.Domain;
using Funzone.UserAccess.Infrastructure.EventBus;
using Microsoft.Extensions.Logging;
using Serilog.Extensions.Logging;
using ILogger = Serilog.ILogger;

namespace Funzone.UserAccess.Infrastructure
{
    public class UserAccessStartup
    {
        public static void Initialize(
            string connectionString,
            ILogger logger,
            IEventBus eventBus)
        {
            var containerBuilder = new ContainerBuilder();

            var loggerFactory = new LoggerFactory(new[]
            {
                new SerilogLoggerProvider(logger)
            });

            containerBuilder.RegisterModule(new DataAccessModule(connectionString, loggerFactory));
            containerBuilder.RegisterModule(new DomainModule());
            containerBuilder.RegisterModule(new EventBusModule());
        }
    }
}