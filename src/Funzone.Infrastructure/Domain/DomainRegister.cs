using Autofac;
using Funzone.Application.Users;
using Funzone.Application.Zones;
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