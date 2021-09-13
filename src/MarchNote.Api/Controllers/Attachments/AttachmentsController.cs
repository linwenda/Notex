using System;
using System.Threading.Tasks;
using MarchNote.Application.Attachments.Commands;
using MarchNote.Application.Configuration.Responses;
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

        [HttpPost]
        [ProducesDefaultResponseType(typeof(MarchNoteResponse<Guid>))]
        [RequestSizeLimit(1024 * 1024 * 2)] //2MB
        public async Task<IActionResult> Upload(IFormFile formFile)
        {
            var response = await _mediator.Send(new AddAttachmentCommand(formFile));
            return Ok(response);
        }
    }
}