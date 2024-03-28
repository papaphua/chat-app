using System.Net;
using System.Net.Http.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace ChatApp.Client.App.Auth;

public sealed class AuthStateProvider(
    HttpClient client,
    ILogger<AuthStateProvider> logger)
    : AuthenticationStateProvider
{
    private static readonly TimeSpan UserCacheRefreshInterval
        = TimeSpan.FromSeconds(60);

    private ClaimsPrincipal _cachedUser = new(new ClaimsIdentity());

    private DateTimeOffset _userLastCheck
        = DateTimeOffset.FromUnixTimeSeconds(0);

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        return new AuthenticationState(await GetUser());
    }

    private async ValueTask<ClaimsPrincipal> GetUser(bool useCache = true)
    {
        var now = DateTimeOffset.Now;
        if (useCache && now < _userLastCheck + UserCacheRefreshInterval)
        {
            logger.LogDebug("Taking user from cache");
            return _cachedUser;
        }

        logger.LogDebug("Fetching user");
        _cachedUser = await FetchUser();
        _userLastCheck = now;

        return _cachedUser;
    }

    private async Task<ClaimsPrincipal> FetchUser()
    {
        try
        {
            logger.LogInformation("Fetching user information.");
            var response = await client.GetAsync("bff/user?slide=false");

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var claims = await response.Content.ReadFromJsonAsync<List<ClaimRecord>>();

                var identity = new ClaimsIdentity(
                    nameof(AuthStateProvider),
                    "name",
                    "role");

                foreach (var claim in claims!) identity.AddClaim(new Claim(claim.Type, claim.Value.ToString()!));

                return new ClaimsPrincipal(identity);
            }
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Fetching user failed.");
        }

        return new ClaimsPrincipal(new ClaimsIdentity());
    }

    private record ClaimRecord(string Type, object Value);
}