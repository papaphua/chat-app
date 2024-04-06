using System.Net.Http.Json;
using System.Text.Json;
using ChatApp.Client.Core;
using ChatApp.Client.Core.Paging;
using ChatApp.Client.Dtos;

namespace ChatApp.Client.Services.DirectService;

public sealed class DirectService(HttpClient http)
    : IDirectService
{
    public async Task<PagedResponse<MessageDto>> GetAllMessagesAsync(Guid directId, MessageParameters parameters)
    {
        var response =
            await http.GetAsync(
                $"api/direct/{directId}?PageSize={parameters.PageSize}&CurrentPage={parameters.CurrentPage}");

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

    public async Task<MessageDto> AddMessageAsync(Guid directId, NewMessageDto dto)
    {
        var response = await http.PostAsJsonAsync($"api/direct/{directId}/message", dto);
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