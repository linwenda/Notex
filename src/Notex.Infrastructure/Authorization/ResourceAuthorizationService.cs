using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Notex.Core.Authorization;
using Notex.Core.Configuration;
using Notex.Core.Exceptions;
using Notex.Core.Lifetimes;

namespace Notex.Infrastructure.Authorization;

public class ResourceAuthorizationService : IResourceAuthorizationService, IScopedLifetime
{
    private readonly ICurrentUser _currentUser;
    private readonly IAuthorizationService _authorizationService;

    public ResourceAuthorizationService(
        ICurrentUser currentUser,
        IAuthorizationService authorizationService)
    {
        _currentUser = currentUser;
        _authorizationService = authorizationService;
    }

    public async Task CheckAsync(object resource, IAuthorizationRequirement requirement)
    {
        var claimIdentity = new ClaimsIdentity(new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, _currentUser.Id.ToString())
        });

        var result = await _authorizationService
            .AuthorizeAsync(new ClaimsPrincipal(claimIdentity), resource, requirement);

        if (!result.Succeeded)
        {
            throw new ResourceAuthorizationException(resource.GetType().Name);
        }
    }
}