using ChatApp.Server.Api.Core.Abstractions;
using ChatApp.Server.Api.Core.Extensions;
using ChatApp.Server.Application.Directs;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Server.Api.Controllers;

[Route("api/direct")]
public sealed class DirectController(
    IDirectService directService)
    : ApiController
{
    [HttpGet("{directId:guid}")]
    public async Task<IResult> GetDirect(Guid directId)
    {
        var result = await directService.GetDirectAsync(UserId, directId);

        return result.IsSuccess
            ? Results.Ok(result.Value)
            : result.ToProblemDetails();
    }

    [HttpPost("{partnerId:guid}")]
    public async Task<IResult> CreateDirect(Guid partnerId)
    {
        var result = await directService.CreateDirectAsync(UserId, partnerId);

        return result.IsSuccess
            ? Results.Ok(result.Value)
            : result.ToProblemDetails();
    }

    [HttpDelete("{directId:guid}")]
    public async Task<IResult> RemoveDirect(Guid directId)
    {
        var result = await directService.RemoveDirectAsync(UserId, directId);

        return result.IsSuccess
            ? Results.Ok()
            : result.ToProblemDetails();
    }

    [HttpDelete("self/{directId:guid}")]
    public async Task<IResult> RemoveDirectForSelf(Guid directId)
    {
        var result = await directService.RemoveDirectForSelfAsync(UserId, directId);

        return result.IsSuccess
            ? Results.Ok()
            : result.ToProblemDetails();
    }
}