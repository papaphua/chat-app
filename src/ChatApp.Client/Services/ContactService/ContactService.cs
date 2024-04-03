using System.Net.Http.Headers;
using System.Net.Http.Json;
using ChatApp.Client.Dtos;
using Microsoft.AspNetCore.Components.Forms;

namespace ChatApp.Client.Services.ContactService;

public sealed class ContactService(HttpClient http) : IContactService
{
    public async Task<ContactDto> GetContactAsync(Guid contactId)
    {
        return await http.GetFromJsonAsync<ContactDto>($"api/contact/{contactId}");
    }

    public async Task AddContactAsync(Guid partnerId, ContactNameDto dto)
    {
        await http.PostAsJsonAsync($"api/contact/{partnerId}", dto);
    }

    public async Task RemoveContactAsync(Guid contactId)
    {
        await http.DeleteAsync($"api/contact/{contactId}");
    }

    public async Task UpdateNameAsync(Guid contactId, ContactNameDto dto)
    {
        await http.PutAsJsonAsync($"api/contact/{contactId}/name", dto);
    }

    public async Task AddAvatarAsync(Guid contactId, IBrowserFile file)
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
        
        await http.PutAsync($"api/contact/{contactId}/avatar", content);
    }

    public async Task RemoveAvatarAsync(Guid contactId)
    {
        await http.DeleteAsync($"api/contact/{contactId}/avatar");
    }
}