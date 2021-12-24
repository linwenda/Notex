using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartNote.Application.NoteComments.Commands;
using SmartNote.Application.NoteComments.Queries;
using SmartNote.Application.NoteCooperations.Commands;
using SmartNote.Application.NoteCooperations.Queries;
using SmartNote.Application.Notes.Commands;
using SmartNote.Application.Notes.Queries;

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

        [HttpGet]
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new DeleteNoteCommand(id));
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNote([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new GetNoteQuery(id));
            return Ok(response);
        }

        [HttpGet("{id}/histories")]
        public async Task<IActionResult> GetNoteHistories([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new GetNoteHistoriesQuery(id));
            return Ok(response);
        }

        [HttpPost("{id}/fork")]
        public async Task<IActionResult> ForkNote([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new ForkNoteCommand(id));
            return Ok(response);
        }

        [HttpPost("{id}/publish")]
        public async Task<IActionResult> PublishNote([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new PublishNoteCommand(id));
            return Ok(response);
        }

        [HttpGet("{id}/comments")]
        public async Task<IActionResult> GetComments([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new GetNoteCommentsQuery(id));
            return Ok(response);
        }

        [HttpPost("{id}/comments")]
        public async Task<IActionResult> AddComment([FromRoute] Guid id, [FromBody] string content)
        {
            var response = await _mediator.Send(new AddNoteCommentCommand(id, content));
            return Ok(response);
        }

        [HttpPost("comments/{commentId}/reply")]
        public async Task<IActionResult> AddReply([FromRoute] Guid commentId, [FromBody] string reply)
        {
            var response = await _mediator.Send(new AddNoteCommentReplyCommand(commentId, reply));
            return Ok(response);
        }

        [HttpDelete("comments/{id}")]
        public async Task<IActionResult> DeleteComment([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new DeleteNoteCommentCommand(id));
            return Ok(response);
        }

        [HttpGet("{id}/cooperations")]
        public async Task<IActionResult> GetCooperations([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new GetNoteCooperationsQuery(id));
            return Ok(response);
        }

        [HttpPost("{id}/cooperations")]
        public async Task<IActionResult> ApplyForNoteCooperation([FromRoute] Guid id, [FromBody] string comment)
        {
            var response = await _mediator.Send(new ApplyForNoteCooperationCommand(id, comment));
            return Ok(response);
        }

        [HttpGet("cooperations")]
        public async Task<IActionResult> GetUserNoteCooperations()
        {
            var response = await _mediator.Send(new GetUserNoteCooperationsQuery());
            return Ok(response);
        }

        [HttpGet("cooperations/{cooperationId}")]
        public async Task<IActionResult> GetNoteCooperationById([FromRoute] Guid cooperationId)
        {
            var response = await _mediator.Send(new GetNoteCooperationByIdQuery(cooperationId));
            return Ok(response);
        }

        [HttpPost("cooperations/{cooperationId}/approve")]
        public async Task<IActionResult> ApproveNoteCooperation([FromRoute] Guid cooperationId)
        {
            var response = await _mediator.Send(new ApproveNoteCooperationCommand(cooperationId));
            return Ok(response);
        }

        [HttpPost("cooperations/{cooperationId}/reject")]
        public async Task<IActionResult> RejectNoteCooperation(
            [FromRoute] Guid cooperationId,
            [FromQuery] string rejectReason)
        {
            var response = await _mediator.Send(new RejectNoteCooperationCommand(cooperationId, rejectReason));
            return Ok(response);
        }
    }
}