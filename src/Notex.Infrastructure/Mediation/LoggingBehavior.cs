using MediatR;
using Microsoft.Extensions.Logging;
using Notex.Core.Extensions;

namespace Notex.Infrastructure.Mediation;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger) => _logger = logger;

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        _logger.LogInformation("----- Handling message {MessageName} ({@Message})", request.GetGenericTypeName(),
            request);
        
        var response = await next();

        _logger.LogInformation("----- Message {MessageName} handled", request.GetGenericTypeName());

        return response;
    }
}