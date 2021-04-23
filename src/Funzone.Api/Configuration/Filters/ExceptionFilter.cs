using System.Net;
using Funzone.Application.Configuration.Exceptions;
using Funzone.Domain.SeedWork;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Funzone.Api.Configuration.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<ExceptionFilter> _logger;

        public ExceptionFilter(IWebHostEnvironment env, ILogger<ExceptionFilter> logger)
        {
            _env = env;
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(new EventId(context.Exception.HResult),
                context.Exception,
                context.Exception.Message);
            
            var json = new JsonErrorResponse
            {
                Messages = new[] { context.Exception.Message }
            };
            
            if (context.Exception.GetType() == typeof(NotFoundException))
            {
                context.Result = new NotFoundObjectResult(json);
                context.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
            }
            
            else if (context.Exception.GetType() == typeof(ValidationException))
            {
                context.Result = new BadRequestObjectResult(json);
                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            }

            else if (context.Exception.GetType() == typeof(BusinessRuleValidationException))
            {
                context.Result = new ConflictObjectResult(json);
                context.HttpContext.Response.StatusCode = StatusCodes.Status409Conflict;
            }
            else
            {
                json.Messages = new[] {"An error occurred. Try it again."};
                if (_env.IsDevelopment())
                {
                    json.DeveloperMessage = context.Exception;
                }

                context.Result = new InternalServerErrorObjectResult(json);
                context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            }
            
            context.ExceptionHandled = true;
        }
    }
}