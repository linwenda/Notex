using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notex.Messages.Comments.Commands;

namespace Notex.Api.Controllers.Comments;

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
    public async Task<IActionResult> EditAsync([FromRoute] Guid id, [FromBody] EditCommentRequest model)
    {
        await _mediator.Send(new EditCommentCommand(id, model.Text));
        return Ok();
    }

    [HttpPost("reply")]
    public async Task<IActionResult> AddReplyAsync([FromRoute] Guid id, [FromBody] AddCommentReplyRequest model)
    {
        await _mediator.Send(new AddCommentReplyCommand(id, model.Text));
        return Ok();
    }
}