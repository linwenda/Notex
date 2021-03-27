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

namespace Funzone.Services.Albums.Infrastructure
{
    public class AlbumsStartup
    {
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
            container.RegisterModule(new EventBusModule(eventBus));

            return new AutofacServiceProvider(container.Build());
        }
    }
}