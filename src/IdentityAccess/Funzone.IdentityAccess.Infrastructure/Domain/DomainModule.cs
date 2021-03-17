using Autofac;
using Funzone.BuildingBlocks.Infrastructure;
using Funzone.IdentityAccess.Application.DomainServices;
using Funzone.IdentityAccess.Domain.Users;
using Funzone.IdentityAccess.Infrastructure.Domain.Users;

namespace Funzone.IdentityAccess.Infrastructure.Domain
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