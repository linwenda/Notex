using Autofac;
using Funzone.BuildingBlocks.Infrastructure.EventBus;
using Funzone.Services.Albums.Infrastructure.DataAccess;
using Funzone.Services.Albums.Infrastructure.Domain;
using Funzone.Services.Albums.Infrastructure.EventBus;
using Funzone.Services.Albums.Infrastructure.Mediator;
using Microsoft.Extensions.Logging;
using Serilog.Extensions.Logging;
using ILogger = Serilog.ILogger;

namespace Funzone.Services.Albums.Infrastructure
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