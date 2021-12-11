using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartNote.Api.Controllers.Models;
using SmartNote.Core.Application.Notes.Contracts;
using SmartNote.Core.Application.Spaces.Contracts;
using SmartNote.Core.Domain.Spaces;

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
        
        [HttpGet("{id}")]
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
                Visibility = Enum.Parse<Visibility>(request.Visibility),
                BackgroundImageId =  request.BackgroundImageId
            });

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSpace([FromRoute] Guid id, [FromBody] UpdateSpaceRequest request)
        {
            var response = await _mediator.Send(new UpdateSpaceCommand(
                id,
                request.Name,
                Enum.Parse<Visibility>(request.Visibility),
                request.BackgroundImageId));

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpace([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new DeleteSpaceCommand(id));
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