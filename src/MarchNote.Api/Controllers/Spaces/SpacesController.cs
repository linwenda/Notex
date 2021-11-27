using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MarchNote.Application.Notes.Queries;
using MarchNote.Application.Spaces.Commands;
using MarchNote.Application.Spaces.Queries;
using MarchNote.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarchNote.Api.Controllers.Spaces
{
    [Authorize]
    [Route("api/spaces")]
    public class SpacesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SpacesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetSpaces()
        {
            var response = await _mediator.Send(new GetDefaultSpacesQuery());
            return Ok(response);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSpaceById([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new GetSpaceByIdQuery(id));
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSpaces(CreateSpaceRequest request)
        {
            var createSpaceResponse = await _mediator.Send(new CreateSpaceCommand
            {
                Name = request.Name,
                Visibility = Enum.Parse<Visibility>(request.Visibility),
            });

            return Ok(createSpaceResponse);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpace([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new DeleteSpaceCommand(id));
            return Ok(response);
        }

        [HttpPost("{id}/rename")]
        public async Task<IActionResult> RenameScope([FromRoute] Guid id, [FromBody] string name)
        {
            var response = await _mediator.Send(new RenameSpaceCommand(id, name));
            return Ok(response);
        }

        [HttpPost("{id}/background")]
        public async Task<IActionResult> UpdateBackground([FromRoute] Guid id, [FromBody] Guid backgroundImageId)
        {
            var response = await _mediator.Send(new UpdateSpaceBackgroundCommand(id, backgroundImageId));
            return Ok(response);
        }

        [HttpGet("{id}/folders")]
        public async Task<IActionResult> GetFolderSpaces([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new GetFolderSpacesQuery(id));
            return Ok(response);
        }

        [HttpPost("{id}/folders")]
        public async Task<IActionResult> AddFolderSpace([FromRoute] Guid id, [FromBody] string name)
        {
            var response = await _mediator.Send(new AddFolderSpaceCommand(id, name));
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