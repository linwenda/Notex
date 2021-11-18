using System;
using System.Security.Authentication;
using System.Threading.Tasks;
using MarchNote.Application.Attachments.Commands;
using MarchNote.Application.Attachments.Queries;
using MarchNote.Application.Configuration.Exceptions;
using MarchNote.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarchNote.Api.Controllers.Attachments
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