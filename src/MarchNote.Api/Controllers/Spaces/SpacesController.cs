using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MarchNote.Application.Configuration.Responses;
using MarchNote.Application.Notes.Queries;
using MarchNote.Application.Spaces.Commands;
using MarchNote.Application.Spaces.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MarchNote.Api.Controllers.Spaces
{
    [Route("api/spaces")]
    public class SpacesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SpacesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesDefaultResponseType(typeof(MarchNoteResponse<IEnumerable<SpaceDto>>))]
        public async Task<IActionResult> GetSpaces()
        {
            var response = await _mediator.Send(new GetDefaultSpacesQuery());
            return Ok(response);
        }

        [HttpPost]
        [ProducesDefaultResponseType(typeof(MarchNoteResponse<Guid>))]
        public async Task<IActionResult> CreateSpaces([FromBody] CreateSpaceCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [ProducesDefaultResponseType(typeof(MarchNoteResponse))]
        public async Task<IActionResult> DeleteSpace([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new DeleteSpaceCommand(id));
            return Ok(response);
        }
        
        [HttpPost("{id}/rename")]
        [ProducesDefaultResponseType(typeof(MarchNoteResponse))]
        public async Task<IActionResult> RenameScope([FromRoute] Guid id, [FromBody]string name)
        {
            var response = await _mediator.Send(new RenameSpaceCommand(id, name));
            return Ok(response);
        }

        [HttpGet("{id}/folders")]
        [ProducesDefaultResponseType(typeof(MarchNoteResponse<IEnumerable<SpaceDto>>))]
        public async Task<IActionResult> GetFolderSpaces([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new GetFolderSpacesQuery(id));
            return Ok(response);
        }

        [HttpPost("{id}/folders")]
        [ProducesDefaultResponseType(typeof(MarchNoteResponse<Guid>))]
        public async Task<IActionResult> AddFolderSpace([FromRoute] Guid id, [FromBody] string name)
        {
            var response = await _mediator.Send(new AddFolderSpaceCommand(id, name));
            return Ok(response);
        }

        [HttpPost("{id}/move")]
        [ProducesDefaultResponseType(typeof(MarchNoteResponse))]
        public async Task<IActionResult> MoveSpaceFolder([FromRoute] Guid id, [FromBody] Guid destSpaceId)
        {
            var response = await _mediator.Send(new MoveSpaceCommand(id, destSpaceId));
            return Ok(response);
        }

        [HttpGet("{id}/notes")]
        public async Task<IActionResult> GetSpaceNotes([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new GetNotesBySpaceIdQuery(id));
            return Ok(response);
        }
    }
}