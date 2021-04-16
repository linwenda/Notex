using System.Threading.Tasks;
using Funzone.Application.Commands.Users.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Funzone.Api.Controllers.Users
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("registration")]
        [HttpPost]
        public async Task<IActionResult> RegisterUserByEmail([FromBody] RegisterUserByEmailCommand request)
        {
            await _mediator.Send(request);
            return Ok();
        }
    }
}