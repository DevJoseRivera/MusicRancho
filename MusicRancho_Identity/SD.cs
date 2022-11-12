using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using IdentityModel;

namespace MusicRancho_Identity
{
    public static class SD
    {
        public const string Admin = "admin";
        public const string Customer = "customer";
        public const string Employee = "employee";

        public static IEnumerable<IdentityResource> IdentityResources =>
            //https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/identity/
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile(),
            };
        public static IEnumerable<ApiScope> ApiScopes =>

            new List<ApiScope>
            {
                new ApiScope("music", "Music Server"),
                new ApiScope(name: "read",   displayName: "Read your data."),
                new ApiScope(name: "write",  displayName: "Write your data."),
                new ApiScope(name: "delete", displayName: "Delete your data.")
            };

        //https://docs.duendesoftware.com/identityserver/v6/fundamentals/clients/

        // apps that are requesting  tokens from identity server to access the api
        public static IEnumerable<Client> Cleints =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "service.client",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "api1", "api2.read_only" }
                },
                new Client
                {
                    ClientId = "music",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowedScopes = { "music",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        JwtClaimTypes.Role
                    },
                    RedirectUris={ "https://localhost:7002/signin-oidc" },
                    PostLogoutRedirectUris={"https://localhost:7002/signout-callback-oidc" },
                },
                new Client
                {
                    ClientId = "music.angular",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowedScopes= {
                        "music",
                        "read",
                        "write",
                        "delete",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        JwtClaimTypes.Role
                    },
                    Claims =
                    {
                         new ClientClaim(JwtClaimTypes.Role, Admin),
                         new ClientClaim(JwtClaimTypes.Role, Employee)                          
                    },
                    RedirectUris = { "https://localhost:44427/auth-callback" },
                    PostLogoutRedirectUris = { "https://localhost:44427" },
                    AllowedCorsOrigins = { "https://localhost:44427" },
                    AllowAccessTokensViaBrowser = true,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    AlwaysSendClientClaims = true,
                }
            };
    }
}
