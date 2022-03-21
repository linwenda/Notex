using Microsoft.AspNetCore.Authorization;
using Notex.Core.Authorization;
using Notex.Core.Authorization.Handlers;

namespace Notex.Api.Identity;

public static class AuthorizationExtensions
{
    public static IServiceCollection AddCustomAuthorization(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddAuthorization(options =>
        {
            options.AddPolicy("UpdatePolicy", policy => policy.Requirements.Add(CommonOperations.Update));
            options.AddPolicy("DeletePolicy", policy => policy.Requirements.Add(CommonOperations.Delete));
        });

        serviceCollection.AddSingleton<IAuthorizationHandler, SpaceAuthorizationHandler>();
        //serviceCollection.AddSingleton<IAuthorizationHandler, NoteAuthorizationHandler>();
        //serviceCollection.AddScoped<IAuthorizationHandler, CommentAuthorizationHandler>();

        return serviceCollection;
    }
}