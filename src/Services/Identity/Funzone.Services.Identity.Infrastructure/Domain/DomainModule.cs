using Autofac;
using Funzone.BuildingBlocks.Infrastructure;
using Funzone.Services.Identity.Application.DomainServices;
using Funzone.Services.Identity.Domain.Users;
using Funzone.Services.Identity.Infrastructure.Domain.Users;

namespace Funzone.Services.Identity.Infrastructure.Domain
{
    public class DomainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<DomainEventsDispatcher>()
                .As<IDomainEventsDispatcher>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<UserRepository>()
                .As<IUserRepository>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<UserCounter>()
                .As<IUserCounter>()
                .InstancePerLifetimeScope();
        }
    }
}