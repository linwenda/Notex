using Funzone.IdentityAccess.Application.Users.RegisterUser;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MediatR;

namespace Funzone.Api.IdentityAccess
{
    [Route("api/identityAccess")]
    [ApiController]
    public class IdentityAccessController : ControllerBase
    {
        private readonly IMediator _mediator;

        public IdentityAccessController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("registration")]
        public async Task<IActionResult> RegisterUser(RegisterUserWithEmailCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }
    }
}