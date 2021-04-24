using Autofac;
using Funzone.Application.DomainServices;
using Funzone.Domain.Users;
using Funzone.Domain.Zones;

namespace Funzone.Infrastructure.Domain
{
    public static class DomainRegister
    {
        public static ContainerBuilder RegisterDomainService(this ContainerBuilder builder)
        {
            builder.RegisterType<UserChecker>()
                .As<IUserChecker>();

            builder.RegisterType<ZoneCounter>()
                .As<IZoneCounter>();

            return builder;
        }
    }
}