using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notex.Api.Models;
using Notex.Messages.Comments.Commands;

namespace Notex.Api.Controllers;

[Authorize]
[Route("comments/{id:guid}")]
public class CommentController : ControllerBase
{
    private readonly IMediator _mediator;

    public CommentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        await _mediator.Send(new DeleteCommentCommand(id));
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> EditAsync([FromRoute] Guid id, [FromBody] EditCommentModel model)
    {
        await _mediator.Send(new EditCommentCommand(id, model.Text));
        return Ok();
    }

    [HttpPost("reply")]
    public async Task<IActionResult> AddReplyAsync([FromRoute] Guid id, [FromBody] AddCommentReplyModel model)
    {
        await _mediator.Send(new AddCommentReplyCommand(id, model.Text));
        return Ok();
    }
}