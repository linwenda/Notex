using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using IdentityServer4.AccessTokenValidation;
using IdentityServer4.Validation;

namespace Notex.Api.Identity;

public static class AuthenticationExtensions
{
    public static void AddCustomIdentityServer(this IServiceCollection serviceCollection, IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        var builder = serviceCollection.AddIdentityServer()
            .AddInMemoryIdentityResources(IdentityServerConfig.GetIdentityResources())
            .AddInMemoryApiResources(IdentityServerConfig.GetApis())
            .AddInMemoryApiScopes(IdentityServerConfig.GetApiScopes())
            .AddInMemoryClients(IdentityServerConfig.GetClients())
            .AddInMemoryPersistedGrants()
            .AddInMemoryCaching()
            .AddProfileService<ProfileService>();

        if (environment.IsDevelopment())
        {
            builder.AddDeveloperSigningCredential();
        }
        else
        {
            builder.AddSigningCredential(new X509Certificate2(
                configuration.GetValue<string>("IdentityServer:Certificates:Path"),
                configuration.GetValue<string>("IdentityServer:Certificates:Secret")));
        }

        serviceCollection.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();

        serviceCollection.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
            .AddIdentityServerAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme, x =>
            {
                x.Authority = configuration.GetValue<string>("IdentityServer:Authority");
                x.ApiName = configuration.GetValue<string>("IdentityServer:ApiName");
                x.RequireHttpsMetadata = configuration.GetValue<bool>("IdentityServer:RequireHttpsMetadata");
                x.RoleClaimType = ClaimTypes.Role;
            });
    }
}