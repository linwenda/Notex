using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Notex.Core.Exceptions;
using Notex.Core.Identity;

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

        switch (context.Exception)
        {
            case InvalidCommandException:
                HandleInvalidCommandException(context);
                break;
            case ResourceAuthorizationException:
                HandleResourceAuthorizationException(context);
                break;
            case EntityNotFoundException:
                HandleEntityNotFoundException(context);
                break;
            case BusinessException:
                HandleBusinessException(context);
                break;
            default:
                HandleInternalServerError(context);
                break;
        }

        context.ExceptionHandled = true;
    }


    private static void HandleEntityNotFoundException(ExceptionContext context)
    {
        var exception = (EntityNotFoundException)context.Exception;

        var details = new ProblemDetails
        {
            Status = StatusCodes.Status404NotFound,
            Title = "The specified resource was not found.",
            Detail = exception.Message
        };

        context.Result = new NotFoundObjectResult(details);
    }

    private static void HandleResourceAuthorizationException(ExceptionContext context)
    {
        var details = new ProblemDetails
        {
            Status = StatusCodes.Status403Forbidden,
            Title = "Forbidden",
            Detail = context.Exception.Message
        };

        context.Result = new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status403Forbidden
        };
    }

    private static void HandleInvalidCommandException(ExceptionContext context)
    {
        var exception = (InvalidCommandException)context.Exception;

        var problemDetails = new ValidationProblemDetails(exception.Errors);

        context.Result = new BadRequestObjectResult(problemDetails);
    }

    private static void HandleBusinessException(ExceptionContext context)
    {
        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status409Conflict,
            Title = "Business validation",
            Detail = context.Exception.Message
        };

        context.Result = new ObjectResult(problemDetails);
    }

    private static void HandleInternalServerError(ExceptionContext context)
    {
        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "Internal error",
            Detail = "An error occur.Try it again."
        };
        
        context.Result = new ObjectResult(problemDetails);
    }
}