using Autofac;
using Autofac.Extensions.DependencyInjection;
using Funzone.BuildingBlocks.Infrastructure.EventBus;
using Funzone.Services.Identity.Infrastructure.DataAccess;
using Funzone.Services.Identity.Infrastructure.Domain;
using Funzone.Services.Identity.Infrastructure.EventBus;
using Funzone.Services.Identity.Infrastructure.Logging;
using Funzone.Services.Identity.Infrastructure.Mediator;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Extensions.Logging;
using System;

namespace Funzone.Services.Identity.Infrastructure
{
    public class IdentityStartup
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
            container.RegisterModule(new LoggingModule(logger));
            container.RegisterModule(new EventBusModule(eventBus));

            return new AutofacServiceProvider(container.Build());
        }
    }
}