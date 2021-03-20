using System.Threading.Tasks;
using Funzone.IdentityAccess.Application.Commands.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Funzone.IdentityAccess.Api.Users
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

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Testing()
        {
            await Task.CompletedTask;
            return Ok(new {Test = "testing"});
        }
    }
}