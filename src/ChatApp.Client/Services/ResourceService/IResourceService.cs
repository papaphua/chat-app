using ChatApp.Client.Dtos;

namespace ChatApp.Client.Services.ResourceService;

public interface IResourceService
{
    Task<ResourceDto> GetResourceAsync(Guid id);
}