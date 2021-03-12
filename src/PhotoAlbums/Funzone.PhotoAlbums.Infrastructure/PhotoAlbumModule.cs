using Autofac;
using Funzone.BuildingBlocks.Infrastructure.EventBus;
using Funzone.PhotoAlbums.Infrastructure.DataAccess;
using Funzone.PhotoAlbums.Infrastructure.Domain;
using Funzone.PhotoAlbums.Infrastructure.EventBus;
using Funzone.PhotoAlbums.Infrastructure.Mediator;
using Microsoft.Extensions.Logging;
using Serilog.Extensions.Logging;
using ILogger = Serilog.ILogger;

namespace Funzone.PhotoAlbums.Infrastructure
{
    public class PhotoAlbumModule : Autofac.Module
    {
        private readonly string _connectionString;
        private readonly ILogger _logger;
        private readonly IEventBus _eventBus;

        public PhotoAlbumModule(
            string connectionString, 
            ILogger logger, 
            IEventBus eventBus)
        {
            _connectionString = connectionString;
            _logger = logger;
            _eventBus = eventBus;
        }

        protected override void Load(ContainerBuilder builder)
        {
            var loggerFactory = new LoggerFactory(new[]
            {
                new SerilogLoggerProvider(_logger)
            });

            builder.RegisterModule(new DataAccessModule(_connectionString, loggerFactory));
            builder.RegisterModule(new DomainModule());
            builder.RegisterModule(new MediatorModule());
            builder.RegisterModule(new EventBusModule(_eventBus));
        }
    }
}