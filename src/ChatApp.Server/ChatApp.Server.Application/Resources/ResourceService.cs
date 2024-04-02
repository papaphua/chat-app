using AutoMapper;
using ChatApp.Server.Application.Shared.Dtos;
using ChatApp.Server.Domain.Core.Abstractions.Results;
using ChatApp.Server.Domain.Resources.Errors;
using ChatApp.Server.Domain.Resources.Repositories;

namespace ChatApp.Server.Application.Resources;

public sealed class ResourceService(IResourceRepository resourceRepository, IMapper mapper)
    : IResourceService
{
    public async Task<Result<ResourceDto>> GetResourceAsync(Guid resourceId)
    {
        var resource = await resourceRepository.GetByIdAsync(resourceId);

        return resource is not null
            ? Result<ResourceDto>.Success(mapper.Map<ResourceDto>(resource))
            : Result<ResourceDto>.Failure(ResourceErrors.NotFound);
    }
}