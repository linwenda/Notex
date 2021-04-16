using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Funzone.Application.Configuration;
using Funzone.Infrastructure.DataAccess;
using Funzone.Infrastructure.Logging;
using Funzone.Infrastructure.Processing;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Funzone.Infrastructure
{
    public static class FunzoneStartup
    {
        public static IServiceProvider Initialize(
            IServiceCollection services,
            string connectionString,
            IExecutionContextAccessor executionContextAccessor,
            ILogger logger)
        {
            var builder = new ContainerBuilder();

            builder.Populate(services);
            builder.RegisterModule(new MediatorModule());
            builder.RegisterModule(new LoggingModule(logger));
            builder.RegisterModule(new DataAccessModule(connectionString));
            builder.RegisterModule(new ProcessingModule(executionContextAccessor));

            var buildContainer = builder.Build();

            CompositionRoot.SetContainer(buildContainer);
            
            return new AutofacServiceProvider(buildContainer);
        }
    }
}