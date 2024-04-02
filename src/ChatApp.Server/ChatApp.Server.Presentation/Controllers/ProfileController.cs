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
    public async Task<IResult> UpdateUserName(UserNameDto dto)
    {
        var result = await profileService.UpdateUserNameAsync(UserId, dto);

        return result.IsSuccess
            ? Results.Ok()
            : result.ToProblemDetails();
    }

    [HttpPost("avatar")]
    public async Task<IResult> AddAvatar([FromForm(Name = "files")] IFormFile file)
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
    public async Task<IResult> SendChangeEmailToken(EmailDto dto)
    {
        var result = await profileService.SendChangeEmailTokenAsync(UserId, dto);

        return result.IsSuccess
            ? Results.Ok()
            : result.ToProblemDetails();
    }

    [HttpPut("email")]
    public async Task<IResult> ChangeEmail(EmailChangeDto dto)
    {
        var result = await profileService.ChangeEmailAsync(UserId, dto);

        return result.IsSuccess
            ? Results.Ok()
            : result.ToProblemDetails();
    }

    [HttpPost("phone/token")]
    public async Task<IResult> SendChangePhoneToken(PhoneNumberDto dto)
    {
        var result = await profileService.SendChangePhoneTokenAsync(UserId, dto);

        return result.IsSuccess
            ? Results.Ok()
            : result.ToProblemDetails();
    }

    [HttpPut("phone")]
    public async Task<IResult> ChangePhone(PhoneNumberChangeDto dto)
    {
        var result = await profileService.ChangePhoneAsync(UserId, dto);

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