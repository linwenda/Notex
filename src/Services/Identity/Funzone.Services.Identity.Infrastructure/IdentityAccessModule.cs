using Autofac;
using Funzone.BuildingBlocks.Infrastructure.EventBus;
using Funzone.Services.Identity.Infrastructure.DataAccess;
using Funzone.Services.Identity.Infrastructure.Domain;
using Funzone.Services.Identity.Infrastructure.EventBus;
using Funzone.Services.Identity.Infrastructure.Logging;
using Funzone.Services.Identity.Infrastructure.Mediator;
using Microsoft.Extensions.Logging;
using Serilog.Extensions.Logging;
using ILogger = Serilog.ILogger;

namespace Funzone.Services.Identity.Infrastructure
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