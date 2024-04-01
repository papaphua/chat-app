using System.Net.Http.Json;
using ChatApp.Client.Dtos;

namespace ChatApp.Client.Services.ProfileService;

public sealed class ProfileService(HttpClient http) : IProfileService
{
    public async Task<ProfileDto> GetProfileAsync()
    {
        return await http.GetFromJsonAsync<ProfileDto>("api/profile");
    }

    public async Task UpdateNameAsync(ProfileNameDto dto)
    {
        await http.PutAsJsonAsync("api/profile/name", dto);
    }

    public async Task UpdateUserName(UserNameDto dto)
    {
        await http.PutAsJsonAsync("api/profile/username", dto);
    }
}