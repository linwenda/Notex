using Funzone.IdentityAccess.Application.Users.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Funzone.IdentityAccess.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("registrations")]
        [HttpPost]
        public async Task<IActionResult> RegisterUser(RegisterUserWithEmailCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }
    }
}
