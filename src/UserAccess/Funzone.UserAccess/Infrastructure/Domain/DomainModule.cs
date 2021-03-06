using Autofac;
using Funzone.UserAccess.Domain.Users;
using Funzone.UserAccess.Infrastructure.Domain.Users;

namespace Funzone.UserAccess.Infrastructure.Domain
{
    public class DomainModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserRepository>()
                .As<IUserRepository>()
                .InstancePerLifetimeScope();
        }
    }
}