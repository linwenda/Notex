using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartNote.Api.Controllers.Models;
using SmartNote.Application.NoteComments.Commands;
using SmartNote.Application.NoteComments.Queries;
using SmartNote.Application.Notes.Commands;
using SmartNote.Application.Notes.Queries;
using SmartNote.Domain.Notes.Blocks;

namespace SmartNote.Api.Controllers
{
    [Authorize]
    [Route("api/notes")]
    public class NotesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NotesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetNotes()
        {
            var response = await _mediator.Send(new GetNotesQuery());
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNote([FromBody] CreateNoteCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateNote([FromRoute] Guid id, [FromBody] UpdateNoteRequest request)
        {
            var blocks = request.Blocks.Select(b => Block.Of(b.Id, b.Type, b.Data.GetRawText())).ToList();

            var response = await _mediator.Send(new UpdateNoteCommand(id, request.Title, blocks));

            return Ok(response);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteNote([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new DeleteNoteCommand(id));
            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetNote([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new GetNoteQuery(id));
            return Ok(response);
        }

        [HttpGet("{id:guid}/histories")]
        public async Task<IActionResult> GetNoteHistories([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new GetNoteHistoriesQuery(id));
            return Ok(response);
        }

        [HttpPost("{id:guid}/fork")]
        public async Task<IActionResult> ForkNote([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new ForkNoteCommand(id));
            return Ok(response);
        }

        [HttpPost("{id:guid}/publish")]
        public async Task<IActionResult> PublishNote([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new PublishNoteCommand(id));
            return Ok(response);
        }

        [HttpGet("{id:guid}/comments")]
        public async Task<IActionResult> GetComments([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new GetNoteCommentsQuery(id));
            return Ok(response);
        }

        [HttpPost("{id:guid}/comments")]
        public async Task<IActionResult> AddComment([FromRoute] Guid id, [FromBody] string content)
        {
            var response = await _mediator.Send(new AddNoteCommentCommand(id, content));
            return Ok(response);
        }

        [HttpPost("comments/{commentId:guid}/reply")]
        public async Task<IActionResult> AddReply([FromRoute] Guid commentId, [FromBody] string reply)
        {
            var response = await _mediator.Send(new AddNoteCommentReplyCommand(commentId, reply));
            return Ok(response);
        }

        [HttpDelete("comments/{commentId:guid}")]
        public async Task<IActionResult> DeleteComment([FromRoute] Guid commentId)
        {
            var response = await _mediator.Send(new DeleteNoteCommentCommand(commentId));
            return Ok(response);
        }
    }
}