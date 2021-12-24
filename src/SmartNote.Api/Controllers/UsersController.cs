using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartNote.Application.Users.Commands;
using SmartNote.Application.Users.Queries;

namespace SmartNote.Api.Controllers;

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
    public async Task<IActionResult> GetCurrentUserInfo()
    {
        var response = await _mediator.Send(new GetCurrentUserQuery());
        return Ok(response);
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut("password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }
}