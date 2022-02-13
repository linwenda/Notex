﻿using FluentValidation;
using MediatR;
using Serilog;
using SmartNote.Application.Configuration.Extensions;
using ValidationException = SmartNote.Application.Configuration.Exceptions.ValidationException;

namespace SmartNote.Infrastructure.Mediator
{
    public class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
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

            _logger.Information("----- Validating request {RequestType}", typeName);

            var failures = _validators
                .Select(v => v.Validate(request))
                .SelectMany(result => result.Errors)
                .Where(error => error != null)
                .ToList();

            if (failures.Any())
            {
                _logger.Warning("Validation errors - {RequestType} - Command: {@Request} - Errors: {@ValidationErrors}",
                    typeName, request, failures);

                throw new ValidationException(failures);
            }

            return await next();
        }
    }
}