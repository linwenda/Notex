using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartNote.Api.Controllers.Models;
using SmartNote.Application.Notes.Queries;
using SmartNote.Application.Spaces.Commands;
using SmartNote.Application.Spaces.Queries;
using SmartNote.Domain.Spaces;

namespace SmartNote.Api.Controllers
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

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetSpaceById([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new GetSpaceByIdQuery(id));
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSpaces([FromBody] CreateSpaceRequest request)
        {
            var response = await _mediator.Send(new CreateSpaceCommand
            {
                Name = request.Name,
                Visibility = request.Visibility,
                BackgroundImageId = request.BackgroundImageId
            });

            return Ok(response);
        }

        [HttpPatch("{id:guid}")]
        public async Task<IActionResult> UpdateSpace([FromRoute] Guid id, [FromBody] UpdateSpaceRequest request)
        {
            var response = await _mediator.Send(new UpdateSpaceCommand(
                id,
                request.Name,
                (Visibility)request.Visibility,
                request.BackgroundImageId));

            return Ok(response);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteSpace([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new DeleteSpaceCommand(id));
            return Ok(response);
        }

        [HttpGet("{id:guid}/folders")]
        public async Task<IActionResult> GetFolderSpaces([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new GetFolderSpacesQuery(id));
            return Ok(response);
        }

        [HttpPost("{id:guid}/folders")]
        public async Task<IActionResult> AddFolderSpace([FromRoute] Guid id, [FromBody] string name)
        {
            var response = await _mediator.Send(new AddFolderSpaceCommand(id, name));
            return Ok(response);
        }

        [HttpGet("{id:guid}/notes")]
        public async Task<IActionResult> GetSpaceNotes([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new GetNotesBySpaceIdQuery(id));
            return Ok(response);
        }
    }
}