using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notex.Messages.Comments.Commands;
using Notex.Messages.Comments.Queries;
using Notex.Messages.MergeRequests.Queries;
using Notex.Messages.Notes.Commands;
using Notex.Messages.Notes.Queries;

namespace Notex.Api.Controllers.Notes;

[Authorize]
[Route("notes")]
public class NoteController : ControllerBase
{
    private readonly IMediator _mediator;

    public NoteController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetNoteQuery(id), cancellationToken));
    }

    [HttpGet("{id:guid}/history")]
    public async Task<IActionResult> GetHistoryAsync(Guid id, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetNoteHistoriesQuery(id), cancellationToken));
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateNoteCommand command)
    {
        return Ok(await _mediator.Send(command));
    }

    [HttpPost("{id:guid}/restore")]
    public async Task<IActionResult> RestoreAsync([FromRoute] Guid id, [FromBody] RestoreNoteRequest model)
    {
        await _mediator.Send(new RestoreNoteCommand(id, model.NoteHistoryId));
        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _mediator.Send(new DeleteNoteCommand(id));
        return Ok();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> EditAsync([FromRoute] Guid id, [FromBody] EditNoteRequest model)
    {
        await _mediator.Send(new EditNoteCommand(id, model.Title, model.Content, model.Comment));
        return Ok();
    }

    [HttpPost("{id:guid}/publish")]
    public async Task<IActionResult> PublishAsync(Guid id)
    {
        await _mediator.Send(new PublishNoteCommand(id));
        return Ok();
    }

    [HttpPut("{id:guid}/tags")]
    public async Task<IActionResult> UpdateTagsAsync([FromRoute] Guid id, [FromBody] UpdateNoteTagsRequest model)
    {
        await _mediator.Send(new UpdateNoteTagsCommand(id, model.Tags ?? new List<string>()));
        return Ok();
    }

    [HttpPost("{id:guid}/clone")]
    public async Task<IActionResult> CloneAsync([FromRoute] Guid id, [FromBody] CloneNoteRequest model)
    {
        return Ok(await _mediator.Send(new CloneNoteCommand(id, model.SpaceId)));
    }

    [HttpGet("{id:guid}/comments")]
    public async Task<IActionResult> GetCommentsAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetCommentsQuery
        {
            EntityId = id.ToString(),
            EntityType = "Note"
        }, cancellationToken));
    }

    [HttpPost("{id:guid}/comments")]
    public async Task<IActionResult> CreateCommentAsync([FromRoute] Guid id, [FromBody] AddNoteCommentRequest model)
    {
        return Ok(await _mediator.Send(new AddNoteCommentCommand
        {
            Text = model.Text,
            NoteId = id
        }));
    }

    [HttpGet("{id:guid}/merge-requests")]
    public async Task<IActionResult> GetMergeRequestsAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetNoteMergeRequestsQuery(id), cancellationToken));
    }
}