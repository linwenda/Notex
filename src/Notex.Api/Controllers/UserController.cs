using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notex.Core.Authorization;
using Notex.Core.Queries;
using Notex.Messages.Users.Commands;

namespace Notex.Api.Controllers;

[Authorize]
[Route("users")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IUserQuery _userQuery;

    public UserController(IMediator mediator,IUserQuery userQuery)
    {
        _mediator = mediator;
        _userQuery = userQuery;
    }

    [Route("me")]
    [HttpGet]
    public async Task<IActionResult> GetCurrentUserAsync()
    {
        return Ok(await _userQuery.GetCurrentUserAsync());
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterUserCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut("password")]
    public async Task<IActionResult> ChangePasswordAsync([FromBody] ChangePasswordCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProfileAsync([FromBody] UpdateProfileCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [Authorize(Roles = AuthorizationConstants.Roles.Administrator)]
    [HttpPut("roles")]
    public async Task<IActionResult> UpdateRolesAsync([FromBody] UpdateUserRolesCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }
}