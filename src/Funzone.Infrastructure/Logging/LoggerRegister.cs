using Autofac;
using Serilog;

namespace Funzone.Infrastructure.Logging
{
    public static class LoggerRegister
    {
        public static ContainerBuilder RegisterLogging(this ContainerBuilder builder, ILogger logger)
        {
            builder.RegisterInstance(logger)
                .As<ILogger>()
                .SingleInstance();

            return builder;
        }
    }
}