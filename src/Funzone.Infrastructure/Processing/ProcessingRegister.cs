using Autofac;
using Funzone.Application.Configuration;
using Funzone.Domain.Users;

namespace Funzone.Infrastructure.Processing
{
    public static class ProcessingRegister
    {
        public static ContainerBuilder RegisterProcessing(this ContainerBuilder builder,
            IExecutionContextAccessor executionContextAccessor)
        {
            builder.RegisterInstance(executionContextAccessor)
                .As<IExecutionContextAccessor>()
                .SingleInstance();

            builder.RegisterType<UserContext>()
                .As<IUserContext>()
                .InstancePerLifetimeScope();

            return builder;
        }
    }
}