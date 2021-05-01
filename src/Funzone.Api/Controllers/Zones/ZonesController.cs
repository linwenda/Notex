using System;
using System.Threading.Tasks;
using Funzone.Application.ZoneMembers.Commands;
using Funzone.Application.Zones.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Funzone.Api.Controllers.Zones
{
    [Route("api/zones")]
    [ApiController]
    public class ZonesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ZonesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateZone(CreateZoneCommand command)
        {
            var zoneId = await _mediator.Send(command);
            return Created("", zoneId);
        }

        [HttpPut("{zoneId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> EditZone([FromRoute] Guid zoneId, [FromBody] EditZoneRequest request)
        {
            await _mediator.Send(new EditZoneCommand(zoneId, request.Description, request.AvatarUrl));
            return Ok();
        }


        [HttpPost("{zoneId}/close")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CloseZone(Guid zoneId)
        {
            await _mediator.Send(new CloseZoneCommand(zoneId));
            return Ok();
        }

        [HttpPost("{zoneId}/join")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> JoinZone(Guid zoneId)
        {
            await _mediator.Send(new JoinZoneCommand(zoneId));
            return NoContent();
        }

        [HttpPost("{zoneId}/leave")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> LeaveZone(Guid zoneId)
        {
            await _mediator.Send(new LeaveZoneCommand(zoneId));
            return Ok();
        }
    }
}