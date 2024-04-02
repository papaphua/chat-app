using ChatApp.Server.Application.Shared.Dtos;
using ChatApp.Server.Domain.Core.Abstractions.Results;

namespace ChatApp.Server.Application.Resources;

public interface IResourceService
{
    Task<Result<ResourceDto>> GetResourceAsync(Guid resourceId);
}