using System.Net.Http.Json;
using ChatApp.Client.Dtos;

namespace ChatApp.Client.Services.ResourceService;

public sealed class ResourceService(HttpClient http)
    : IResourceService
{
    public async Task<ResourceDto> GetResourceAsync(Guid id)
    {
        return await http.GetFromJsonAsync<ResourceDto>($"apr/resources/{id}");
    }
}