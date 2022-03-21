using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Notex.Core.Aggregates.Spaces;

namespace Notex.Core.Authorization.Handlers;

public class SpaceAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, Space>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
        OperationAuthorizationRequirement requirement,
        Space resource)
    {
        var currentUserIdString = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(currentUserIdString))
        {
            return;
        }

        if (resource.GetCreatorId() == Guid.Parse(currentUserIdString))
        {
            context.Succeed(requirement);
        }

        await Task.CompletedTask;
    }
}