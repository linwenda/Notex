using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notex.Messages.Comments.Commands;
using Notex.Messages.MergeRequests.Commands;
using Notex.Messages.MergeRequests.Queries;

namespace Notex.Api.Controllers.MergeRequests;

[Authorize]
[Route("merge-requests")]
public class MergeRequestController : ControllerBase
{
    private readonly IMediator _mediator;

    public MergeRequestController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetAsync([FromRoute] Guid id,CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetMergeRequestQuery(id), cancellationToken));
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateMergeRequestCommand command)
    {
        return Ok(await _mediator.Send(command));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateMergeRequestRequest model)
    {
        await _mediator.Send(new UpdateMergeRequestCommand(id, model.Title, model.Description));
        return Ok();
    }

    [HttpPost("{id:guid}/merge")]
    public async Task<IActionResult> MergeAsync([FromRoute] Guid id)
    {
        await _mediator.Send(new MergeTheRequestCommand(id));
        return Ok();
    }

    [HttpPost("{id:guid}/close")]
    public async Task<IActionResult> CloseAsync([FromRoute] Guid id)
    {
        await _mediator.Send(new CloseMergeRequestCommand(id));
        return Ok();
    }

    [HttpPost("{id:guid}/reopen")]
    public async Task<IActionResult> ReopenAsync([FromRoute] Guid id)
    {
        await _mediator.Send(new ReopenMergeRequestCommand(id));
        return Ok();
    }

    [HttpPost("{id:guid}/comments")]
    public async Task<IActionResult> AddCommentsAsync([FromRoute] Guid id, [FromBody] AddMergeRequestCommentRequest model)
    {
        await _mediator.Send(new AddMergeRequestCommentCommand(id, model.Text));
        return Ok();
    }
}