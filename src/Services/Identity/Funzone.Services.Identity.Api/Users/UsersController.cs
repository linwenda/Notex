using System.Threading.Tasks;
using Funzone.BuildingBlocks.Infrastructure.EventBus;
using Funzone.Services.Identity.Application.Commands.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Funzone.Services.Identity.Api.Users
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