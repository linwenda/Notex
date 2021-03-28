using System.Net;
using Funzone.BuildingBlocks.Application.Exceptions;
using Funzone.BuildingBlocks.Domain;
using Funzone.Services.Identity.Api.Configuration.ActionResults;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Funzone.Services.Identity.Api.Configuration.Filters
{
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<HttpGlobalExceptionFilter> _logger;

        public HttpGlobalExceptionFilter(
            IWebHostEnvironment env,
            ILogger<HttpGlobalExceptionFilter> logger)
        {
            _env = env;
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(new EventId(context.Exception.HResult),
                context.Exception,
                context.Exception.Message);

            switch (context.Exception)
            {
                case NotFoundException:
                {
                    var json = new JsonErrorResponse
                    {
                        Messages = new[] {context.Exception.Message}
                    };

                    context.Result = new NotFoundObjectResult(json);
                    context.HttpContext.Response.StatusCode = (int) HttpStatusCode.NotFound;
                    break;
                }
                case BusinessRuleValidationException:
                case ValidationException:
                {
                    var json = new JsonErrorResponse
                    {
                        Messages = new[] {context.Exception.Message}
                    };

                    context.Result = new BadRequestObjectResult(json);
                    context.HttpContext.Response.StatusCode = (int) HttpStatusCode.BadRequest;
                    break;
                }
                default:
                {
                    var json = new JsonErrorResponse
                    {
                        Messages = new[] {"An error occurred. Try it again."}
                    };

                    if (_env.IsDevelopment())
                    {
                        json.DeveloperMessage = context.Exception;
                    }

                    context.Result = new InternalServerErrorObjectResult(json);
                    context.HttpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                    break;
                }
            }

            context.ExceptionHandled = true;
        }
    }
}