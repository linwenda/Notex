using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MarchNote.Application.Configuration.Responses;
using MarchNote.Application.NoteCooperations.Commands;
using MarchNote.Application.NoteCooperations.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MarchNote.Api.Controllers.NoteCooperations
{
    [Route("api/notes/cooperations")]
    public class NoteCooperationsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public NoteCooperationsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet]
        [ProducesDefaultResponseType(typeof(MarchNoteResponse<IEnumerable<NoteCooperationDto>>))]
        public async Task<IActionResult> GetUserNoteCooperations()
        {
            var response = await _mediator.Send(new GetUserNoteCooperationsQuery());
            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesDefaultResponseType(typeof(MarchNoteResponse<NoteCooperationDto>))]
        public async Task<IActionResult> GetNoteCooperationById([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new GetNoteCooperationByIdQuery(id));
            return Ok(response);
        }

        [HttpPost]
        [ProducesDefaultResponseType(typeof(MarchNoteResponse<Guid>))]
        public async Task<IActionResult> ApplyForNoteCooperation([FromBody] ApplyForNoteCooperationCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut("{cooperationId}/approve")]
        [ProducesDefaultResponseType(typeof(MarchNoteResponse<Guid>))]
        public async Task<IActionResult> ApproveNoteCooperation([FromRoute] Guid cooperationId)
        {
            var response = await _mediator.Send(new ApproveNoteCooperationCommand(cooperationId));
            return Ok(response);
        }

        [HttpPut("{cooperationId}/reject")]
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