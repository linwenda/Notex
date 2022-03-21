using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Notex.Core.Exceptions;

namespace Notex.Api.Filters;

public class GlobalExceptionFilter : IExceptionFilter
{
    private readonly ILogger<GlobalExceptionFilter> _logger;

    public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        _logger.LogError(new EventId(context.Exception.HResult),
            context.Exception,
            context.Exception.Message);

        var jsonErrorResponse = new JsonErrorResponse
        {
            Message = context.Exception.Message
        };

        switch (context.Exception)
        {
            case InvalidCommandException:
                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                break;
            case ResourceAuthorizationException:
                context.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                break;
            case EntityNotFoundException:
                context.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                break;
            case BusinessException:
                context.HttpContext.Response.StatusCode = StatusCodes.Status409Conflict;
                break;
            default:
                context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                break;
        }

        context.Result = new ObjectResult(jsonErrorResponse);
    }

    private class JsonErrorResponse
    {
        public string Message { get; set; }
    }
}