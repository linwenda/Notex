using IdentityServer4.AccessTokenValidation;
using IdentityServer4.Validation;

namespace SmartNote.Api.Configuration.Identity
{
    public static class IdentityServerExtension
    {
        public static void AddCustomIdentityServer(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddIdentityServer()
                .AddInMemoryIdentityResources(IdentityServerConfig.GetIdentityResources())
                .AddInMemoryApiResources(IdentityServerConfig.GetApis())
                .AddInMemoryApiScopes(IdentityServerConfig.GetApiScopes())
                .AddInMemoryClients(IdentityServerConfig.GetClients())
                .AddInMemoryPersistedGrants()
                .AddInMemoryCaching()
                .AddProfileService<ProfileService>()
                .AddDeveloperSigningCredential();

            services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();
            
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme, x =>
                {
                    x.Authority = configuration.GetValue<string>("IdentityServer:Authority");
                    x.ApiName = configuration.GetValue<string>("IdentityServer:ApiName");
                    x.RequireHttpsMetadata = configuration.GetValue<bool>("IdentityServer:RequireHttpsMetadata");
                });
        }
    }
}