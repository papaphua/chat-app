using ChatApp.Server.Application.Profiles;
using ChatApp.Server.Application.Profiles.Dtos;
using ChatApp.Server.Presentation.Core.Abstractions;
using ChatApp.Server.Presentation.Core.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Server.Presentation.Controllers;

[Route("api/profile")]
public sealed class ProfileController(
    IProfileService profileService)
    : ApiController
{
    [HttpGet]
    public async Task<IResult> GetProfile()
    {
        var result = await profileService.GetProfileAsync(UserId);

        return result.IsSuccess
            ? Results.Ok(result.Value)
            : result.ToProblemDetails();
    }

    [HttpPut("name")]
    public async Task<IResult> UpdateName(ProfileNameDto dto)
    {
        var result = await profileService.UpdateNameAsync(UserId, dto);

        return result.IsSuccess
            ? Results.Ok()
            : result.ToProblemDetails();
    }

    [HttpPut("username")]
    public async Task<IResult> UpdateUserName(string username)
    {
        var result = await profileService.UpdateUserNameAsync(UserId, username);

        return result.IsSuccess
            ? Results.Ok()
            : result.ToProblemDetails();
    }

    [HttpPost("avatar")]
    public async Task<IResult> AddAvatar(IFormFile file)
    {
        var result = await profileService.AddAvatarAsync(UserId, file);

        return result.IsSuccess
            ? Results.Ok(result.Value)
            : result.ToProblemDetails();
    }

    [HttpDelete("avatar/{resourceId:guid}")]
    public async Task<IResult> RemoveAvatar(Guid resourceId)
    {
        var result = await profileService.RemoveAvatarAsync(UserId, resourceId);

        return result.IsSuccess
            ? Results.Ok()
            : result.ToProblemDetails();
    }

    [HttpPost("email/token")]
    public async Task<IResult> SendChangeEmailToken(string email)
    {
        var result = await profileService.SendChangeEmailTokenAsync(UserId, email);

        return result.IsSuccess
            ? Results.Ok()
            : result.ToProblemDetails();
    }

    [HttpPut("email")]
    public async Task<IResult> ChangeEmail(string email, string token)
    {
        var result = await profileService.ChangeEmailAsync(UserId, email, token);

        return result.IsSuccess
            ? Results.Ok()
            : result.ToProblemDetails();
    }
    
    [HttpPost("phone/token")]
    public async Task<IResult> SendChangePhoneToken(string number)
    {
        var result = await profileService.SendChangePhoneTokenAsync(UserId, number);

        return result.IsSuccess
            ? Results.Ok()
            : result.ToProblemDetails();
    }

    [HttpPut("phone")]
    public async Task<IResult> ChangePhone(string number, string token)
    {
        var result = await profileService.ChangePhoneAsync(UserId, number, token);

        return result.IsSuccess
            ? Results.Ok()
            : result.ToProblemDetails();
    }

    [HttpPut("password")]
    public async Task<IResult> ChangePassword(NewPasswordDto dto)
    {
        var result = await profileService.ChangePasswordAsync(UserId, dto);

        return result.IsSuccess
            ? Results.Ok()
            : result.ToProblemDetails();
    }
}