using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MarchNote.Application.Configuration.Responses;
using MarchNote.Application.NoteComments.Commands;
using MarchNote.Application.NoteComments.Queries;
using MarchNote.Application.NoteCooperations.Commands;
using MarchNote.Application.NoteCooperations.Queries;
using MarchNote.Application.Notes.Commands;
using MarchNote.Application.Notes.Queries;
using MarchNote.Domain.Notes.ReadModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarchNote.Api.Controllers.Notes
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
        [ProducesDefaultResponseType(typeof(MarchNoteResponse<IEnumerable<NoteReadModel>>))]
        public async Task<IActionResult> GetNotes()
        {
            var response = await _mediator.Send(new GetNotesQuery());
            return Ok(response);
        }

        [HttpPost]
        [ProducesDefaultResponseType(typeof(MarchNoteResponse<Guid>))]
        public async Task<IActionResult> CreateNote([FromBody] CreateNoteCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut("{id}")]
        [ProducesDefaultResponseType(typeof(MarchNoteResponse<Unit>))]
        public async Task<IActionResult> EditNote([FromRoute] Guid id, [FromBody] EditNoteRequest request)
        {
            var response = await _mediator.Send(new EditNoteCommand(id, request.Title, request.Content));
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [ProducesDefaultResponseType(typeof(MarchNoteResponse<Unit>))]
        public async Task<IActionResult> DeleteNote([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new DeleteNoteCommand(id));
            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesDefaultResponseType(typeof(MarchNoteResponse<NoteReadModel>))]
        public async Task<IActionResult> GetNote([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new GetNoteQuery(id));
            return Ok(response);
        }

        [HttpGet("{id}/histories")]
        [ProducesDefaultResponseType(typeof(MarchNoteResponse<NoteHistoryReadModel>))]
        public async Task<IActionResult> GetNoteHistories([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new GetNoteHistoriesQuery(id));
            return Ok(response);
        }

        [HttpPost("{id}/draftOut")]
        [ProducesDefaultResponseType(typeof(MarchNoteResponse<Guid>))]
        public async Task<IActionResult> DraftOutNote([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new DraftOutNoteCommand(id));
            return Ok(response);
        }

        [HttpPost("{id}/publish")]
        [ProducesDefaultResponseType(typeof(MarchNoteResponse<Unit>))]
        public async Task<IActionResult> PublishNote([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new PublishNoteCommand(id));
            return Ok(response);
        }

        [HttpPost("{id}/merge")]
        [ProducesDefaultResponseType(typeof(MarchNoteResponse<Unit>))]
        public async Task<IActionResult> MergeNote([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new MergeNoteCommand(id));
            return Ok(response);
        }

        [HttpGet("{id}/comments")]
        [ProducesDefaultResponseType(typeof(MarchNoteResponse<IEnumerable<NoteCommentDto>>))]
        public async Task<IActionResult> GetComments([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new GetNoteCommentsQuery(id));
            return Ok(response);
        }

        [HttpPost("{id}/comments")]
        [ProducesDefaultResponseType(typeof(MarchNoteResponse<Guid>))]
        public async Task<IActionResult> AddComment([FromRoute] Guid id, [FromBody] string content)
        {
            var response = await _mediator.Send(new AddNoteCommentCommand(id, content));
            return Ok(response);
        }

        [HttpPost("comments/{commentId}/reply")]
        [ProducesDefaultResponseType(typeof(MarchNoteResponse<Guid>))]
        public async Task<IActionResult> AddReply([FromRoute] Guid commentId, [FromBody] string reply)
        {
            var response = await _mediator.Send(new AddNoteCommentReplyCommand(commentId, reply));
            return Ok(response);
        }

        [HttpDelete("comments/{id}")]
        [ProducesDefaultResponseType(typeof(MarchNoteResponse))]
        public async Task<IActionResult> DeleteComment([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new DeleteNoteCommentCommand(id));
            return Ok(response);
        }

        [HttpGet("{id}/cooperations")]
        [ProducesDefaultResponseType(typeof(MarchNoteResponse<IEnumerable<NoteCooperationDto>>))]
        public async Task<IActionResult> GetCooperations([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new GetNoteCooperationsQuery(id));
            return Ok(response);
        }

        [HttpPost("{id}/cooperations")]
        [ProducesDefaultResponseType(typeof(MarchNoteResponse<Guid>))]
        public async Task<IActionResult> ApplyForNoteCooperation([FromRoute] Guid id, [FromBody] string comment)
        {
            var response = await _mediator.Send(new ApplyForNoteCooperationCommand(id, comment));
            return Ok(response);
        }

        [HttpGet("cooperations")]
        [ProducesDefaultResponseType(typeof(MarchNoteResponse<IEnumerable<NoteCooperationDto>>))]
        public async Task<IActionResult> GetUserNoteCooperations()
        {
            var response = await _mediator.Send(new GetUserNoteCooperationsQuery());
            return Ok(response);
        }

        [HttpGet("cooperations/{cooperationId}")]
        [ProducesDefaultResponseType(typeof(MarchNoteResponse<NoteCooperationDto>))]
        public async Task<IActionResult> GetNoteCooperationById([FromRoute] Guid cooperationId)
        {
            var response = await _mediator.Send(new GetNoteCooperationByIdQuery(cooperationId));
            return Ok(response);
        }
        
        [HttpPost("cooperations/{cooperationId}/approve")]
        [ProducesDefaultResponseType(typeof(MarchNoteResponse<Guid>))]
        public async Task<IActionResult> ApproveNoteCooperation([FromRoute] Guid cooperationId)
        {
            var response = await _mediator.Send(new ApproveNoteCooperationCommand(cooperationId));
            return Ok(response);
        }

        [HttpPost("cooperations/{cooperationId}/reject")]
        [ProducesDefaultResponseType(typeof(MarchNoteResponse<Guid>))]
        public async Task<IActionResult> RejectNoteCooperation(
            [FromRoute] Guid cooperationId,
            [FromQuery] string rejectReason)
        {
            var response = await _mediator.Send(new RejectNoteCooperationCommand(cooperationId, rejectReason));
            return Ok(response);
        }
    }
}