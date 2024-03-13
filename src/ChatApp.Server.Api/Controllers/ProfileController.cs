using ChatApp.Server.Api.Core.Abstractions;
using ChatApp.Server.Api.Core.Extensions;
using ChatApp.Server.Application.Profiles;
using ChatApp.Server.Application.Profiles.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Server.Api.Controllers;

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

    [HttpPut("details")]
    public async Task<IResult> UpdateDetails(DetailsDto dto)
    {
        var result = await profileService.UpdateDetailsAsync(UserId, dto);
        
        return result.IsSuccess
            ? Results.Ok(result.Value)
            : result.ToProblemDetails();
    }
}