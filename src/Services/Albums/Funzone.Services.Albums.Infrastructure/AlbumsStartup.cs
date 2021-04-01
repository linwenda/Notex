using Autofac;
using Autofac.Extensions.DependencyInjection;
using Funzone.BuildingBlocks.Infrastructure.EventBus;
using Funzone.Services.Albums.Infrastructure.DataAccess;
using Funzone.Services.Albums.Infrastructure.Domain;
using Funzone.Services.Albums.Infrastructure.EventBus;
using Funzone.Services.Albums.Infrastructure.Mediator;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Extensions.Logging;
using System;
using Funzone.Services.Albums.Infrastructure.Logging;

namespace Funzone.Services.Albums.Infrastructure
{
    public class AlbumsStartup
    {
        private static IContainer _container;

        public static IServiceProvider Initialize(
            IServiceCollection services,
            string connectionString,
            ILogger logger,
            IEventBus eventBus)
        {
            var container = new ContainerBuilder();

            container.Populate(services);

            container.RegisterModule(new DataAccessModule(connectionString, new SerilogLoggerFactory(logger)));
            container.RegisterModule(new DomainModule());
            container.RegisterModule(new MediatorModule());
            container.RegisterModule(new LoggingModule(logger));
            container.RegisterModule(new EventBusModule(eventBus));
            
            _container = container.Build();

            AlbumsContainer.Set(_container);

            return new AutofacServiceProvider(_container);
        }
    }
}