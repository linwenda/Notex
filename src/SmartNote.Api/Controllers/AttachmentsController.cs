using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartNote.Core.Application.Attachments.Commands;
using SmartNote.Core.Application.Attachments.Queries;

namespace SmartNote.Api.Controllers
{
    [Authorize]
    [Route("api/attachments")]
    public class AttachmentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AttachmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new GetAttachmentQuery(id));
            if (response == null) return NoContent();
            
            var file = System.IO.File.OpenRead(response.Path);
            return File(file, response.ContentType);
        }
        
        [AllowAnonymous]
        [HttpGet("images/{id}")]
        public async Task<IActionResult> GetImagesAsync([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new GetAttachmentQuery(id));
            if (response == null) return NoContent();
            
            var file = System.IO.File.OpenRead(response.Path);
            return File(file, response.ContentType);
        }

        [HttpPost]
        [RequestSizeLimit(1024 * 1024 * 2)] //2MB
        public async Task<IActionResult> UploadAsync(IFormFile file)
        {
            var response = await _mediator.Send(new AddAttachmentCommand(file));
            return Ok(response);
        }
    }
}