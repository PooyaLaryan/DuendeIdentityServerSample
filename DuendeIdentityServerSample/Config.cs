using Duende.IdentityModel;
using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace DuendeIdentityServerSample
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources => [
            new IdentityResources.OpenId()
            ];

        public static IEnumerable<ApiScope> ApiScopes => [
            new ApiScope(name:"api1", displayName:"My Api")
            ];

        public static IEnumerable<Client> Clients => [
            new Client{
                ClientId = "client",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret("secret".ToSha256()) },
                AllowedScopes = {"api1",
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Address,
                    "myIdentity",
                    "roles",
                    "api2"
                }
            }
            ];
    }
}
