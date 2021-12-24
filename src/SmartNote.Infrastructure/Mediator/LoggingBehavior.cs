using MediatR;
using Serilog;
using SmartNote.Application.Configuration.Extensions;

namespace SmartNote.Infrastructure.Mediator
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger _logger;

        public LoggingBehavior(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                _logger.Information("----- Handling request {RequestName} ({@Request})", request.GetGenericTypeName(),
                    request);

                var response = await next();

                _logger.Information("----- Request {RequestName} handled", request.GetGenericTypeName());

                return response;
            }
            catch (Exception ex)
            {
                var typeName = request.GetGenericTypeName();
                _logger.Error(ex, "ERROR Handling request for {RequestName} ({@Request})", typeName, request);
                throw;
            }
        }
    }
}