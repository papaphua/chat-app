using System.Net.Http.Headers;
using System.Net.Http.Json;
using ChatApp.Client.Dtos;
using Microsoft.AspNetCore.Components.Forms;

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

    public async Task UpdateUserNameAsync(UserNameDto dto)
    {
        await http.PutAsJsonAsync("api/profile/username", dto);
    }

    public async Task AddAvatarAsync(IBrowserFile file)
    {
        using var content = new MultipartFormDataContent();
        var fileContent =
            new StreamContent(file.OpenReadStream(1024 * 15));
        fileContent.Headers.ContentType =
            new MediaTypeHeaderValue(file.ContentType);

        content.Add(
            content: fileContent,
            name: "files",
            fileName: file.Name);
        
        await http.PostAsync("api/profile/avatar", content);
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