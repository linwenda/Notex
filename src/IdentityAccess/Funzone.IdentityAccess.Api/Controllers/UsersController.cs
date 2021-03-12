using Funzone.IdentityAccess.Application.Users.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Funzone.IdentityAccess.Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("registration")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserWithEmailCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }
    }
}