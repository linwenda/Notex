using System;
using System.Security.Authentication;
using System.Threading.Tasks;
using MarchNote.Application.Attachments.Commands;
using MarchNote.Application.Attachments.Queries;
using MarchNote.Application.Configuration.Exceptions;
using MarchNote.Domain.SeedWork;
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

        [AllowAnonymous]
        [HttpGet("test/{key}")]
        public async Task<IActionResult> Test([FromRoute]int key)
        {
            if (key == 0)
            {
                throw new NotFoundException("entity");
            }

            if (key == 1)
            {
                var exception = new BusinessNewException("test", "test");
                exception.WithData("name", "bruce.l");
                
                throw exception;
            }

            if (key == 2)
            {
                throw new AuthenticationException("no authentication");
            }

            if (key == 3)
            {
                throw new Exception("exception");
            }

            await Task.CompletedTask;
            return Ok(key);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new GetAttachmentQuery(id));
            if (response == null) return NoContent();
            
            var file = System.IO.File.OpenRead(response.Path);
            return File(file, response.ContentType);

        }

        [HttpPost]
        [RequestSizeLimit(1024 * 1024 * 2)] //2MB
        public async Task<IActionResult> Upload(IFormFile file)
        {
            var response = await _mediator.Send(new AddAttachmentCommand(file));
            return Ok(response);
        }
    }
}