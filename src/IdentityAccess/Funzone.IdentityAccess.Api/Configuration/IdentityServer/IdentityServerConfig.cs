using System.Collections.Generic;
using Funzone.IdentityAccess.Application.Commands.Authenticate;
using IdentityServer4;
using IdentityServer4.Models;

namespace Funzone.IdentityAccess.Api.Configuration.IdentityServer
{
    public class IdentityServerConfig
    {
        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
            {
                new ApiResource("funzoneApi", "Funzone Aggregator Api"),
                new ApiResource("identityAccessApi", "Identity Access Api"),
                new ApiResource("photoAlbumsApi", "Photo Albums Api")
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
                        "funzoneApi",
                        "identityAccessApi",
                        "photoAlbumsApi",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                }
            };
        }
    }
}