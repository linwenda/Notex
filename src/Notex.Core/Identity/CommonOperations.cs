using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace Notex.Core.Identity;

public static class CommonOperations
{
    public static OperationAuthorizationRequirement Update = new OperationAuthorizationRequirement
        { Name = nameof(Update) };

    public static OperationAuthorizationRequirement Delete = new OperationAuthorizationRequirement
        { Name = nameof(Delete) };
}