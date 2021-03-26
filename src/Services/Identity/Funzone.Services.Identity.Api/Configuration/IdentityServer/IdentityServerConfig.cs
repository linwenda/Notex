using System.Collections.Generic;
using Funzone.Services.Identity.Application.Commands.Authenticate;
using IdentityServer4;
using IdentityServer4.Models;

namespace Funzone.Services.Identity.Api.Configuration.IdentityServer
{
    public class IdentityServerConfig
    {
        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
            {
                new ApiResource("identity-api", "Identity Api")
                {
                    Scopes = {"identity-api"}
                },
                new ApiResource("albums-api", "Albums Api")
                {
                    Scopes = { "albums-api" }
                }
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope("identity-api"),
                new ApiScope("albums-api")
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource(CustomClaimTypes.Roles, new List<string>
                {
                    CustomClaimTypes.Roles
                })
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "funzone.app",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    ClientSecrets =
                    {
                        new Secret("funzoneSecret".Sha256())
                    },
                    AllowedScopes =
                    {
                        "identity-api",
                        "albums-api",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                }
            };
        }
    }
}