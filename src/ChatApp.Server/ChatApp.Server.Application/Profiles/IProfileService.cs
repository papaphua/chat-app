using ChatApp.Server.Application.Profiles.Dtos;
using ChatApp.Server.Application.Shared.Dtos;
using ChatApp.Server.Domain.Core.Abstractions.Results;
using Microsoft.AspNetCore.Http;

namespace ChatApp.Server.Application.Profiles;

public interface IProfileService
{
    Task<Result<ProfileDto>> GetProfileAsync(Guid userId);

    Task<Result> UpdateNameAsync(Guid userId, ProfileNameDto dto);

    Task<Result> UpdateUserNameAsync(Guid userId, UserNameDto dto);

    Task<Result<AvatarDto>> AddAvatarAsync(Guid userId, IFormFile file);

    Task<Result> RemoveAvatarAsync(Guid userId, Guid resourceId);

    Task<Result> SendChangeEmailTokenAsync(Guid userId, EmailDto dto);

    Task<Result> ChangeEmailAsync(Guid userId, EmailChangeDto dto);

    Task<Result> SendChangePhoneTokenAsync(Guid userId, PhoneNumberDto dto);

    Task<Result> ChangePhoneAsync(Guid userId, PhoneNumberChangeDto dto);

    Task<Result> ChangePasswordAsync(Guid userId, NewPasswordDto dto);
}