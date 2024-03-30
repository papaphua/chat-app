using ChatApp.Server.Application.Profiles.Dtos;
using ChatApp.Server.Application.Shared.Dtos;
using ChatApp.Server.Domain.Core.Abstractions.Results;
using Microsoft.AspNetCore.Http;

namespace ChatApp.Server.Application.Profiles;

public interface IProfileService
{
    Task<Result<ProfileDto>> GetProfileAsync(Guid userId);

    Task<Result> UpdateNameAsync(Guid userId, ProfileNameDto dto);

    Task<Result> UpdateUserNameAsync(Guid userId, string userName);

    Task<Result<AvatarDto>> AddAvatarAsync(Guid userId, IFormFile file);

    Task<Result> RemoveAvatarAsync(Guid userId, Guid resourceId);

    Task<Result> UpdateEmailAsync(Guid userId, string email);
    Task<Result> SendEmailConfirmationAsync(Guid userId);

    Task<Result> ConfirmEmailAsync(Guid userId, string token);
    
    Task<Result> UpdatePhoneAsync(Guid userId, string email);
    Task<Result> SendPhoneConfirmationAsync(Guid userId);

    Task<Result> ConfirmPhoneAsync(Guid userId, string token);

    Task<Result> ChangePasswordAsync(Guid userId, NewPasswordDto dto);
}