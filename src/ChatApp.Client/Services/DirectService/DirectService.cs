using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using ChatApp.Client.Core;
using ChatApp.Client.Core.Paging;
using ChatApp.Client.Dtos;
using Microsoft.AspNetCore.Components.Forms;

namespace ChatApp.Client.Services.DirectService;

public sealed class DirectService(HttpClient http)
    : IDirectService
{
    public async Task<PagedResponse<MessageDto>> GetAllMessagesAsync(Guid directId, MessageParameters parameters)
    {
        var response =
            await http.GetAsync(
                $"api/direct/{directId}/messages?PageSize={parameters.PageSize}&CurrentPage={parameters.CurrentPage}");

        return new PagedResponse<MessageDto>
        {
            Items = await response.Content.ReadFromJsonAsync<List<MessageDto>>(),
            PagedData = JsonSerializer.Deserialize<PagedData>(response.Headers.GetValues("X-PagedData").First())
        };
    }

    public async Task<DirectDto> GetDirectAsync(Guid directId)
    {
        return await http.GetFromJsonAsync<DirectDto>($"api/direct/{directId}");
    }

    public async Task<Guid> CreateDirectAsync(Guid partnerId)
    {
        var response = await http.PostAsync($"api/direct/{partnerId}", null);
        return await response.Content.ReadFromJsonAsync<Guid>();
    }

    public async Task RemoveDirectAsync(Guid directId)
    {
        await http.DeleteAsync($"api/direct/{directId}");
    }

    public async Task RemoveDirectForSelfAsync(Guid directId)
    {
        await http.DeleteAsync($"api/direct/self/{directId}");
    }

    public async Task<MessageDto> AddMessageAsync(Guid directId, string? text, List<IBrowserFile>? files = null)
    {
        var multipartContent = new MultipartFormDataContent();
        if(!string.IsNullOrEmpty(text))
            multipartContent.Add(new StringContent(text), "text");

        if (files != null && files.Count > 0)
        {
            foreach (var file in files)
            {
                var fileContent = new StreamContent(file.OpenReadStream(file.Size));
                fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                {
                    Name = "\"files\"",
                    FileName = $"\"{file.Name}\""
                };
                multipartContent.Add(fileContent);
            }
        }
        
        
        var response = await http.PostAsync($"api/direct/{directId}/message", multipartContent);
        return await response.Content.ReadFromJsonAsync<MessageDto>();
    }

    public async Task RemoveMessageAsync(Guid directId, Guid messageId)
    {
        await http.DeleteAsync($"api/direct/{directId}/message/{messageId}");
    }

    public async Task RemoveMessageForSelf(Guid directId, Guid messageId)
    {
        await http.DeleteAsync($"api/direct/{directId}/message/self/{messageId}");
    }

    public async Task AddReaction(Guid directId, Guid messageId, ReactionType type)
    {
        await http.PostAsJsonAsync($"api/direct/{directId}/message/{messageId}/reaction", type);
    }

    public async Task RemoveReaction(Guid directId, Guid messageId, Guid reactionId)
    {
        await http.DeleteAsync($"api/direct/{directId}/message/{messageId}/reaction/{reactionId}");
    }
}