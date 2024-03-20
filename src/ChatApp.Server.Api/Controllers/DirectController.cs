using AutoMapper;
using ChatApp.Server.Api.Core.Abstractions;
using ChatApp.Server.Api.Core.Extensions;
using ChatApp.Server.Api.Requests;
using ChatApp.Server.Application.Directs;
using ChatApp.Server.Application.Shared.Dtos;
using ChatApp.Server.Domain.Core;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Server.Api.Controllers;

[Route("api/direct")]
public sealed class DirectController(
    IDirectService directService,
    IMapper mapper)
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

    [HttpPost("{directId:guid}/message")]
    public async Task<IResult> AddMessage(Guid directId, [FromForm] NewMessageRequest request)
    {
        var result = await directService.AddMessageAsync(
            UserId,
            directId,
            mapper.Map<NewMessageDto>(request));

        return result.IsSuccess
            ? Results.Ok(result.Value)
            : result.ToProblemDetails();
    }
    
    [HttpDelete("{directId:guid}/message/{messageId:guid}")]
    public async Task<IResult> RemoveMessage(Guid directId, Guid messageId)
    {
        var result = await directService.RemoveMessageAsync(
            UserId,
            directId,
            messageId);

        return result.IsSuccess
            ? Results.Ok(result.Value)
            : result.ToProblemDetails();
    }
    
    [HttpDelete("{directId:guid}/message/self/{messageId:guid}")]
    public async Task<IResult> RemoveMessageForSelf(Guid directId, Guid messageId)
    {
        var result = await directService.RemoveMessageForSelfAsync(
            UserId,
            directId,
            messageId);

        return result.IsSuccess
            ? Results.Ok()
            : result.ToProblemDetails();
    }

    [HttpPost("{directId:guid}/message/{messageId:guid}/reaction")]
    public async Task<IResult> AddReaction(Guid directId, Guid messageId, ReactionType type)
    {
        var result = await directService.AddReactionAsync(
            UserId,
            directId,
            messageId,
            type);
        
        return result.IsSuccess
            ? Results.Ok(result.Value)
            : result.ToProblemDetails();
    }
    
    [HttpDelete("{directId:guid}/message/{messageId:guid}/reaction/{reactionId:guid}")]
    public async Task<IResult> RemoveReaction(Guid directId, Guid messageId, Guid reactionId)
    {
        var result = await directService.RemoveReactionAsync(
            UserId,
            directId,
            messageId,
            reactionId);
        
        return result.IsSuccess
            ? Results.Ok(result.Value)
            : result.ToProblemDetails();
    }
}