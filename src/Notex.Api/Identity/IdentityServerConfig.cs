using System.Security.Claims;
using IdentityServer4;
using IdentityServer4.Models;

namespace Notex.Api.Identity;

public static class IdentityServerConfig
{
    public static IEnumerable<ApiResource> GetApis()
    {
        return new List<ApiResource>
        {
            new ApiResource("Notex.Api", "Notex.Api")
            {
                Scopes = { "Notex.Api" }
            }
        };
    }

    public static IEnumerable<ApiScope> GetApiScopes()
    {
        return new List<ApiScope>
        {
            new ApiScope("Notex.Api")
        };
    }

    public static IEnumerable<IdentityResource> GetIdentityResources()
    {
        return new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource(ClaimTypes.Role, new List<string>
            {
                ClaimTypes.Role
            })
        };
    }

    public static IEnumerable<Client> GetClients()
    {
        return new List<Client>
        {
            new Client
            {
                ClientId = "Notex.App",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                AllowOfflineAccess = true,
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                AllowedScopes =
                {
                    "Notex.Api",
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.OfflineAccess,
                    ClaimTypes.Role
                }
            }
        };
    }
}