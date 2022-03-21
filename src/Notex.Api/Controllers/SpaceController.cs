using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notex.Api.Models;
using Notex.Core.Queries;
using Notex.Messages.Spaces.Commands;

namespace Notex.Api.Controllers;

[Authorize]
[Route("spaces")]
public class SpaceController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ISpaceQuery _spaceQuery;
    private readonly INoteQuery _noteQuery;

    public SpaceController(IMediator mediator, ISpaceQuery spaceQuery, INoteQuery noteQuery)
    {
        _mediator = mediator;
        _spaceQuery = spaceQuery;
        _noteQuery = noteQuery;
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetMySpacesAsync()
    {
        return Ok(await _spaceQuery.GetMySpacesAsync());
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetAsync([FromQuery] Guid id)
    {
        return Ok(await _spaceQuery.GetSpaceAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateSpaceCommand request)
    {
        return Ok(await _mediator.Send(request));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateSpaceModel model)
    {
        await _mediator.Send(new UpdateSpaceCommand
        {
            Name = model.Name,
            Visibility = model.Visibility,
            BackgroundImage = model.BackgroundImage,
            SpaceId = id
        });

        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        await _mediator.Send(new DeleteSpaceCommand(id));
        return Ok();
    }

    [HttpGet("{id:guid}/notes")]
    public async Task<IActionResult> GetNotesAsync([FromQuery] Guid id)
    {
        return Ok(await _noteQuery.GetNotesFromSpaceAsync(id));
    }
}