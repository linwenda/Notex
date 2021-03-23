using System.Threading;
using System.Threading.Tasks;
using Funzone.BuildingBlocks.Infrastructure;
using MediatR;
using Serilog;

namespace Funzone.IdentityAccess.Application.Configurations.Behaviours
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger _logger;

        public LoggingBehavior(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            _logger.Information("----- Handling command {CommandName} ({@Command})", request.GetGenericTypeName(),
                request);

            var response = await next();

            _logger.Information("----- Command {CommandName} handled - response: {@Response}",
                request.GetGenericTypeName(), response);

            return response;
        }
    }
}