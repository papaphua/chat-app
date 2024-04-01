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

    public async Task<bool> SendChangeEmailTokenAsync(EmailDto dto)
    {
        var response = await http.PostAsJsonAsync("api/profile/email/token", dto);
        return response.IsSuccessStatusCode;
    }

    public async Task ChangeEmailAsync(EmailChangeDto dto)
    {
        await http.PutAsJsonAsync("api/profile/email", dto);
    }

    public async Task<bool> SendChangePhoneTokenAsync(PhoneNumberDto dto)
    {
        var response = await http.PostAsJsonAsync("api/profile/phone/token", dto);
        return response.IsSuccessStatusCode;
    }

    public async Task ChangePhoneAsync(PhoneNumberChangeDto dto)
    {
        await http.PutAsJsonAsync("api/profile/phone", dto);
    }

    public async Task ChangePasswordAsync(NewPasswordDto dto)
    {
        await http.PutAsJsonAsync("api/profile/password", dto);
    }
}