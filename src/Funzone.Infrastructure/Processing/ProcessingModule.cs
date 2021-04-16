using Autofac;
using Funzone.Application.Configuration;
using Funzone.Domain.Users;

namespace Funzone.Infrastructure.Processing
{
    public class ProcessingModule : Autofac.Module
    {
        private readonly IExecutionContextAccessor _executionContextAccessor;

        public ProcessingModule(IExecutionContextAccessor executionContextAccessor)
        {
            _executionContextAccessor = executionContextAccessor;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(_executionContextAccessor)
                .As<IExecutionContextAccessor>()
                .SingleInstance();

            builder.RegisterType<UserContext>()
                .As<IUserContext>()
                .InstancePerLifetimeScope();
        }
    }
}