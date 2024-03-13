using AutoMapper;
using ChatApp.Server.Application.Profiles.Dtos;
using ChatApp.Server.Domain.Core.Abstractions.Results;
using ChatApp.Server.Domain.Users.Repositories;

namespace ChatApp.Server.Application.Profiles;

public sealed class ProfileService(
    IUserRepository userRepository,
    IMapper mapper)
    : IProfileService
{
    public async Task<Result<ProfileDto>> GetProfileAsync(Guid userId)
    {
        var user = await userRepository.GetByIdAsync(userId, true);

        return Result<ProfileDto>.Success(mapper.Map<ProfileDto>(user));
    }
}