using System.Security.Claims;
using IdentityServer4.Models;
using IdentityServer4.Services;

namespace SmartNote.Api.Configuration.Identity
{
    public class ProfileService : IProfileService
    {
        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            context.IssuedClaims.Add(context.Subject.Claims.Single(x => x.Type == ClaimTypes.Email));
            return Task.CompletedTask;
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            return Task.FromResult(context.IsActive);
        }
    }
}