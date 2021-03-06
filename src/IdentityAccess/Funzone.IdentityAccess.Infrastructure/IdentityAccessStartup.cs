using Autofac;
using Funzone.IdentityAccess.Infrastructure.DataAccess;
using Funzone.IdentityAccess.Infrastructure.Domain;
using Funzone.IdentityAccess.Infrastructure.EventBus;
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
            containerBuilder.RegisterModule(new EventBusModule());
        }
    }
}