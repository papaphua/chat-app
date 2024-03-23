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
}