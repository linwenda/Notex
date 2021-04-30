using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Funzone.Application.Commands;
using Funzone.Application.Configuration.Extensions;
using MediatR;
using Serilog;
using ValidationException = Funzone.Application.Configuration.Exceptions.ValidationException;


namespace Funzone.Application.Configuration.Behaviours
{
    public class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommand<TResponse>
    {
        private readonly ILogger _logger;
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidatorBehavior(
            ILogger logger,
            IEnumerable<IValidator<TRequest>> validators)
        {
            _logger = logger;
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            var typeName = request.GetGenericTypeName();

            _logger.Information("----- Validating command {CommandType}", typeName);

            var failures = _validators
                .Select(v => v.Validate(request))
                .SelectMany(result => result.Errors)
                .Where(error => error != null)
                .ToList();

            if (failures.Any())
            {
                _logger.Warning("Validation errors - {CommandType} - Command: {@Command} - Errors: {@ValidationErrors}",
                    typeName, request, failures);
                throw new ValidationException(failures);
            }

            return await next();
        }
    }
}