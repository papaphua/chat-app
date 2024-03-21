using ChatApp.Server.Application.Profiles.Dtos;
using ChatApp.Server.Application.Shared.Dtos;
using ChatApp.Server.Domain.Core.Abstractions.Results;

namespace ChatApp.Server.Application.Profiles;

public interface IProfileService
{
    Task<Result<ProfileDto>> GetProfileAsync(Guid userId);

    Task<Result<ProfileDetailsDto>> UpdateDetailsAsync(Guid userId, ProfileDetailsDto dto);

    Task<Result<string>> UpdateUserNameAsync(Guid userId, string userName);

    Task<Result<AvatarDto>> AddAvatarAsync(Guid userId, NewResourceDto dto);

    Task<Result> RemoveAvatarAsync(Guid userId, Guid resourceId);
}