using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Funzone.Aggregator.IdentityAccess
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IIdentityAccessService _identityAccessService;

        public UsersController(IIdentityAccessService identityAccessService)
        {
            _identityAccessService = identityAccessService;
        }

        [Route("registration")]
        [HttpPost]
        public async Task<IActionResult> RegisterUserWithEmail([FromBody]RegisterUserWithEmailRequest request)
        {
            await _identityAccessService.RegisterUserWithEmail(request);
            return Ok();
        }
    }
}