using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using MarchNote.Application.Configuration.Exceptions;
using MarchNote.Domain.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MarchNote.Api.Filters
{
    public class ExceptionFilter : IExceptionFilter, IAsyncExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var exceptionResult = new ExceptionResult
            {
                Message = context.Exception.Message
            };
            
            context.HttpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

            switch (context.Exception)
            {
                case NotFoundException:
                    context.HttpContext.Response.StatusCode = (int) HttpStatusCode.NotFound;
                    break;
                case ValidationException validationException:
                    exceptionResult.ValidationErrors = validationException.Errors;
                    context.HttpContext.Response.StatusCode = (int) HttpStatusCode.BadRequest;
                    break;
                case BusinessException businessException:
                    exceptionResult.Code = businessException.Code;
                    context.HttpContext.Response.StatusCode = (int) HttpStatusCode.Conflict;
                    break;
            }

            context.Result = new ObjectResult(exceptionResult);
        }

        public Task OnExceptionAsync(ExceptionContext context)
        {
            OnException(context);
            return Task.CompletedTask;
        }
    }

    public class ExceptionResult
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public IDictionary<string, string[]> ValidationErrors { get; set; }
    }
}