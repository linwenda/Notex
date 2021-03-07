using Autofac;
using Funzone.IdentityAccess.Application.Users;
using Funzone.IdentityAccess.Domain.Users;
using Funzone.IdentityAccess.Infrastructure.Domain.Users;

namespace Funzone.IdentityAccess.Infrastructure.Domain
{
    public class DomainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
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