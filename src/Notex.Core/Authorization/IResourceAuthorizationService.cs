using Microsoft.AspNetCore.Authorization;

namespace Notex.Core.Authorization;

public interface IResourceAuthorizationService
{
    Task CheckAsync(object resource, IAuthorizationRequirement requirement);
}