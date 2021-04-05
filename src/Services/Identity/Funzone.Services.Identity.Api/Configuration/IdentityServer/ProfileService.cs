using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Funzone.Services.Identity.Application.Commands.Authenticate;
using IdentityServer4.Models;
using IdentityServer4.Services;

namespace Funzone.Services.Identity.Api.Configuration.IdentityServer
{
    public class ProfileService : IProfileService
    {
        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            context.IssuedClaims.AddRange(context.Subject.Claims.Where(x => x.Type == ClaimTypes.Role).ToList());
            context.IssuedClaims.Add(context.Subject.Claims.Single(x => x.Type == ClaimTypes.Name));
            context.IssuedClaims.Add(context.Subject.Claims.Single(x => x.Type == ClaimTypes.Email));

            return Task.CompletedTask;
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            return Task.FromResult(context.IsActive);
        }
    }
}