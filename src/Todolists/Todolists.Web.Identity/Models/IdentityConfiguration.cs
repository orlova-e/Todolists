using IdentityServer4.Models;
using System.Security.Claims;

namespace Todolists.Web.Identity.Models
{
    public static class IdentityConfiguration
    {
        public static IEnumerable<ApiScope> GetApiScopes(IdentityOptions options)
            => new List<ApiScope>
            {
                new ApiScope
                {
                    Name = options.Scope.Name,
                    UserClaims = new []
                    {
                        ClaimTypes.Name,
                        options.Scope.UserClaimsClientId
                    }
                }
            };

        public static IEnumerable<Client> GetClients(IdentityOptions options)
            => new List<Client>
            {
                new Client
                {
                    ClientId = options.Client.ClientId,
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes = { options.Client.AllowedScope },
                    RequireClientSecret = options.Client.RequireClientSecret,
                    AllowOfflineAccess = options.Client.AllowOfflineAccess,
                    AccessTokenLifetime = (int) TimeSpan.FromHours(options.Client.AccessTokenLifetimeHours).TotalSeconds
                }
            };
        
        public static IEnumerable<ApiResource> GetApiResources()
            => new List<ApiResource>
            {
                new ApiResource()
                {
                    UserClaims =
                    {
                        ClaimTypes.Name,
                        "client_id"
                    }
                },
            };
    }
}
