using System;
using System.Threading.Tasks;
using MarchNote.Application.Configuration.Responses;
using MarchNote.Application.Users.Command;
using MarchNote.Application.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarchNote.Api.Controllers.Users
{
    [Authorize]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("me")]
        [ProducesDefaultResponseType(typeof(MarchNoteResponse<UserDto>))]
        public async Task<IActionResult> GetCurrentUserInfo()
        {
            var response = await _mediator.Send(new GetCurrentUserQuery());
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesDefaultResponseType(typeof(MarchNoteResponse<Guid>))]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut("password")]
        [ProducesDefaultResponseType(typeof(MarchNoteResponse<Unit>))]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut]
        [ProducesDefaultResponseType(typeof(MarchNoteResponse))]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}