using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notex.Messages.Notes.Queries;
using Notex.Messages.Spaces.Commands;
using Notex.Messages.Spaces.Queries;

namespace Notex.Api.Controllers.Spaces;

[Authorize]
[Route("spaces")]
public class SpaceController : ControllerBase
{
    private readonly IMediator _mediator;

    public SpaceController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetMySpacesAsync(CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetMySpacesQuery(), cancellationToken));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetAsync([FromQuery] Guid id, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetMySpacesQuery(), cancellationToken));
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateSpaceCommand request)
    {
        return Ok(await _mediator.Send(request));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateSpaceRequest model)
    {
        await _mediator.Send(new UpdateSpaceCommand
        {
            Name = model.Name,
            Visibility = model.Visibility,
            Cover = model.Cover,
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
}