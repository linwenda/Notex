using System;
using System.Threading.Tasks;
using Funzone.Application.Commands.ZoneRules;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Funzone.Api.Controllers.ZoneRules
{
    [Route("api/zones/rules")]
    [ApiController]
    public class ZoneRulesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ZoneRulesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> AddZoneRule([FromBody] AddZoneRuleCommand command)
        {
            await _mediator.Send(command);
            return new CreatedResult("", "");
        }

        [HttpPut("{zoneRuleId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> EditZoneRule(
            [FromRoute] Guid zoneRuleId,
            [FromBody] EditZoneRuleRequest request)
        {
            await _mediator.Send(new EditZoneRuleCommand(zoneRuleId, request.Title, request.Description, request.Sort));
            return Ok();
        }
        
        [HttpDelete("{zoneRuleId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteZoneRule(
            [FromRoute] Guid zoneRuleId)
        {
            await _mediator.Send(new DeleteZoneRuleCommand(zoneRuleId));
            return Ok();
        }
    }
}