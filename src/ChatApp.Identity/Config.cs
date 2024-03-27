using Duende.IdentityServer.Models;

namespace ChatApp.Identity;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId()
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ("api", "API")
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            new ()
            {
                ClientId = "client",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets =
                {
                    new Secret(Environment.GetEnvironmentVariable("CLIENT_SECRET").Sha256())
                }
            }
        };
}