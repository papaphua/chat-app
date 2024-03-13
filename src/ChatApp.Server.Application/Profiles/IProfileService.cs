using ChatApp.Server.Application.Profiles.Dtos;
using ChatApp.Server.Domain.Core.Abstractions.Results;

namespace ChatApp.Server.Application.Profiles;

public interface IProfileService
{
    Task<Result<ProfileDto>> GetProfileAsync(Guid userId);

    Task<Result<DetailsDto>> UpdateDetailsAsync(Guid userId, DetailsDto dto);
}