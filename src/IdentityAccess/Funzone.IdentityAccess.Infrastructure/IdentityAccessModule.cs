using Autofac;
using Funzone.BuildingBlocks.Infrastructure.EventBus;
using Funzone.IdentityAccess.Infrastructure.DataAccess;
using Funzone.IdentityAccess.Infrastructure.Domain;
using Funzone.IdentityAccess.Infrastructure.EventBus;
using Funzone.IdentityAccess.Infrastructure.Logging;
using Funzone.IdentityAccess.Infrastructure.Mediator;
using Microsoft.Extensions.Logging;
using Serilog.Extensions.Logging;
using ILogger = Serilog.ILogger;

namespace Funzone.IdentityAccess.Infrastructure
{
    public class IdentityAccessModule : Autofac.Module
    {
        private readonly string _connectionString;
        private readonly ILogger _logger;
        private readonly IEventBus _eventBus;

        public IdentityAccessModule(
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
            builder.RegisterModule(new LoggingModule(_logger));
            builder.RegisterModule(new EventBusModule(_eventBus));
        }
    }
}