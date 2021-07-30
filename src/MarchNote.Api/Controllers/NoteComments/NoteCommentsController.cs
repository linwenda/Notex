using System;
using System.Threading.Tasks;
using MarchNote.Application.Configuration.Responses;
using MarchNote.Application.NoteComments.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MarchNote.Api.Controllers.NoteComments
{
    [Route("api/notes/comments")]
    public class NoteCommentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NoteCommentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesDefaultResponseType(typeof(MarchNoteResponse<Guid>))]
        public async Task<IActionResult> AddComment([FromBody] AddNoteCommentCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("{id}/reply")]
        [ProducesDefaultResponseType(typeof(MarchNoteResponse<Guid>))]
        public async Task<IActionResult> AddReply([FromRoute] Guid id, [FromBody] string reply)
        {
            var response = await _mediator.Send(new AddNoteCommentReplyCommand(id, reply));
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [ProducesDefaultResponseType(typeof(MarchNoteResponse))]
        public async Task<IActionResult> DeleteComment([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new DeleteNoteCommentCommand(id));
            return Ok(response);
        }
    }
}