using Autofac;
using Funzone.Application.Configuration;
using Funzone.Infrastructure.DataAccess;
using Funzone.Infrastructure.Domain;
using Funzone.Infrastructure.Logging;
using Funzone.Infrastructure.Processing;
using Serilog;

namespace Funzone.Infrastructure
{
    public class FunzoneModule : Module
    {
        private readonly string _connectionString;
        private readonly IExecutionContextAccessor _executionContextAccessor;
        private readonly ILogger _logger;

        public FunzoneModule(
            string connectionString,
            IExecutionContextAccessor executionContextAccessor,
            ILogger logger)
        {
            _connectionString = connectionString;
            _executionContextAccessor = executionContextAccessor;
            _logger = logger;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterLogging(_logger)
                .RegisterProcessing(_executionContextAccessor)
                .RegisterDatabase(_connectionString)
                .RegisterDomainService()
                .RegisterMediator();
        }
    }
}