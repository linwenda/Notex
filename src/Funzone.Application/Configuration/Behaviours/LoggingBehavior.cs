using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Funzone.Application.Configuration.Extensions;
using MediatR;
using Serilog;

namespace Funzone.Application.Configuration.Behaviours
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly Stopwatch _timer;
        private readonly ILogger _logger;

        public LoggingBehavior(ILogger logger)
        {
            _timer = new Stopwatch();
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            _timer.Start();

            _logger.Information("----- Handling request {RequestName} ({@Request})",
                request.GetGenericTypeName(),
                request);

            var response = await next();
            
            _timer.Stop();

            _logger.Information("----- Request {RequestName} handled - Elapsed Millisecond: {@ElapsedMilliseconds}",
                request.GetGenericTypeName(), _timer.ElapsedMilliseconds);

            return response;
        }
    }
}