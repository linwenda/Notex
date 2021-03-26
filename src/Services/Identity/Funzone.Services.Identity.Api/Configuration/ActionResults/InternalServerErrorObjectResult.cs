using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Funzone.Services.Identity.Api.Configuration.ActionResults
{
    public class InternalServerErrorObjectResult : ObjectResult
    {
        public InternalServerErrorObjectResult(object error)
            : base(error)
        {
            StatusCode = StatusCodes.Status500InternalServerError;
        }
    }
}
