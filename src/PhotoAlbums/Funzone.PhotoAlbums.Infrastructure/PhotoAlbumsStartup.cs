using Autofac;
using Funzone.PhotoAlbums.Infrastructure.DataAccess;
using Funzone.PhotoAlbums.Infrastructure.Domain;
using Funzone.PhotoAlbums.Infrastructure.EventBus;
using Funzone.PhotoAlbums.Infrastructure.Mediator;
using Microsoft.Extensions.Logging;
using Serilog.Extensions.Logging;
using ILogger = Serilog.ILogger;

namespace Funzone.PhotoAlbums.Infrastructure
{
    public static class PhotoAlbumsStartup
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
            containerBuilder.RegisterModule(new MediatorModule());
        }
    }
}