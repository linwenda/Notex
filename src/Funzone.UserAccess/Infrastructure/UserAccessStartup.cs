using Autofac;
using Funzone.BuildingBlocks.EventBus.Abstractions;
using Funzone.UserAccess.Infrastructure.DataAccess;
using Funzone.UserAccess.Infrastructure.Domain;
using Funzone.UserAccess.Infrastructure.EventBus;
using Microsoft.Extensions.Logging;

namespace Funzone.UserAccess.Infrastructure
{
    public class UserAccessStartup
    {
        public static void Initialize(
            string connectionString,
            ILoggerFactory loggerFactory,
            IEventBus eventBus)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule(new DataAccessModule(connectionString, loggerFactory));
            containerBuilder.RegisterModule(new DomainModule());
            containerBuilder.RegisterModule(new EventBusModule(eventBus));
        }
    }
}