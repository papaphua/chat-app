using DotNetEnv;
using Duende.IdentityServer.Models;

namespace ChatApp.Identity;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new("api", "API")
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            new()
            {
                ClientId = "WebApp",
                ClientSecrets =
                {
                    new Secret(Env.GetString("CLIENT_SECRET").Sha256())
                },
                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris = { "https://localhost:6001/signin-oidc" },
                FrontChannelLogoutUri = "https://localhost:6001/signout-oidc",
                PostLogoutRedirectUris = { "https://localhost:6001/signout-callback-oidc" },
                AllowOfflineAccess = true,
                AllowedScopes = { "openid", "profile", "remote_api", "api" }
            }
        };
}