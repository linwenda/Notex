using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notex.Api.Models;
using Notex.Core.Aggregates.Notes;
using Notex.Core.Queries;
using Notex.Core.Queries.Models;
using Notex.Messages.Comments.Commands;
using Notex.Messages.Notes.Commands;

namespace Notex.Api.Controllers;

[Authorize]
[Route("notes")]
public class NoteController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly INoteQuery _noteQuery;
    private readonly ICommentQuery _commentQuery;
    private readonly IMergeRequestQuery _mergeRequestQuery;

    public NoteController(
        IMediator mediator,
        INoteQuery noteQuery,
        ICommentQuery commentQuery,
        IMergeRequestQuery mergeRequestQuery)
    {
        _mediator = mediator;
        _noteQuery = noteQuery;
        _commentQuery = commentQuery;
        _mergeRequestQuery = mergeRequestQuery;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetAsync(Guid id)
    {
        return Ok(await _noteQuery.GetNoteAsync(id));
    }

    [HttpGet("{id:guid}/history")]
    public async Task<IActionResult> GetHistoryAsync(Guid id)
    {
        return Ok(await _noteQuery.GetHistoriesAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateNoteCommand command)
    {
        return Ok(await _mediator.Send(command));
    }

    [HttpPost("{id:guid}/restore")]
    public async Task<IActionResult> RestoreAsync([FromRoute] Guid id, [FromBody] RestoreNoteModel model)
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
    public async Task<IActionResult> EditAsync([FromRoute] Guid id, [FromBody] EditNoteModel model)
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
    public async Task<IActionResult> UpdateTagsAsync([FromRoute] Guid id, [FromBody] UpdateNoteTagsModel model)
    {
        await _mediator.Send(new UpdateNoteTagsCommand(id, model.Tags ?? new List<string>()));
        return Ok();
    }

    [HttpPost("{id:guid}/clone")]
    public async Task<IActionResult> CloneAsync([FromRoute] Guid id, [FromBody] CloneNoteModel model)
    {
        return Ok(await _mediator.Send(new CloneNoteCommand(id, model.SpaceId)));
    }

    [HttpGet("{id:guid}/comments")]
    public async Task<IActionResult> GetCommentsAsync([FromRoute] Guid id)
    {
        return Ok(await _commentQuery.GetCommentsAsync(nameof(Note), id.ToString()));
    }

    [HttpPost("{id:guid}/comments")]
    public async Task<IActionResult> CreateCommentAsync([FromRoute] Guid id, [FromBody] AddNoteCommentModel model)
    {
        return Ok(await _mediator.Send(new AddNoteCommentCommand
        {
            Text = model.Text,
            NoteId = id
        }));
    }

    [HttpGet("{id:guid}/merge-requests")]
    public async Task<IActionResult> GetMergeRequestsAsync([FromRoute] Guid id)
    {
        return Ok(await _mergeRequestQuery.GetMergeRequestsFromNoteAsync(id));
    }
}