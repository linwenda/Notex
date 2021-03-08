using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Funzone.Aggregator.IdentityAccess
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IIdentityAccessService _identityAccessService;

        public UsersController(IIdentityAccessService identityAccessService)
        {
            _identityAccessService = identityAccessService;
        }

        [Route("registrations")]
        [HttpPost]
        public async Task<IActionResult> RegisterUser(RegisterUserRequest registerUserRequest)
        {
            await _identityAccessService.RegisterUser(registerUserRequest);
            return Ok();
        }
    }
}