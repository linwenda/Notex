using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notex.Api.Models;
using Notex.Core.Queries;
using Notex.Messages.Comments.Commands;
using Notex.Messages.MergeRequests.Commands;

namespace Notex.Api.Controllers;

[Authorize]
[Route("merge-requests")]
public class MergeRequestController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMergeRequestQuery _mergeRequestQuery;

    public MergeRequestController(IMediator mediator, IMergeRequestQuery mergeRequestQuery)
    {
        _mediator = mediator;
        _mergeRequestQuery = mergeRequestQuery;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetAsync([FromRoute] Guid id)
    {
        return Ok(await _mergeRequestQuery.GetMergeRequestAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateMergeRequestCommand command)
    {
        return Ok(await _mediator.Send(command));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateMergeRequestModel model)
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
    public async Task<IActionResult> AddCommentsAsync([FromRoute] Guid id, [FromBody] AddMergeRequestCommentModel model)
    {
        await _mediator.Send(new AddMergeRequestCommentCommand(id, model.Text));
        return Ok();
    }
}