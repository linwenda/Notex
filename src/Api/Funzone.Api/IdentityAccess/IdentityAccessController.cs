using Funzone.IdentityAccess.Application.Users.RegisterUser;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Funzone.IdentityAccess.Infrastructure;
using MediatR;

namespace Funzone.Api.IdentityAccess
{
    [Route("api/identityAccess")]
    [ApiController]
    public class IdentityAccessController : ControllerBase
    {
        [HttpPost("registration")]
        public async Task<IActionResult> RegisterUser(RegisterUserWithEmailCommand command)
        {
            await IdentityAccessExecutor.Execute(command);
            return Ok();
        }
    }
}