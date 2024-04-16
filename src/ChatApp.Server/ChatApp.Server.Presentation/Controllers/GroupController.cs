using ChatApp.Server.Application.Groups;
using ChatApp.Server.Application.Groups.Dtos;
using ChatApp.Server.Application.Shared.Dtos;
using ChatApp.Server.Domain.Core;
using ChatApp.Server.Domain.Shared;
using ChatApp.Server.Presentation.Core.Abstractions;
using ChatApp.Server.Presentation.Core.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ChatApp.Server.Presentation.Controllers;

[Route("api/group")]
public sealed class GroupController(IGroupService groupService)
    : ApiController
{
    [HttpPost]
    public async Task<IResult> CreateGroup(NewGroupDto dto)
    {
        var result = await groupService.CreateGroupAsync(UserId, dto);
        
        return result.IsSuccess
            ? Results.Ok(result.Value)
            : result.ToProblemDetails();
    }

    [HttpPut("{groupId:guid}/info")]
    public async Task<IResult> UpdateGroupInfo(Guid groupId, GroupInfoDto dto)
    {
        var result = await groupService.UpdateGroupInfoAsync(UserId, groupId, dto);
        
        return result.IsSuccess
            ? Results.Ok()
            : result.ToProblemDetails();
    }

    [HttpPost("{groupId:guid}/avatar")]
    public async Task<IResult> AddAvatar(Guid groupId, [FromForm(Name = "files")] IFormFile file)
    {
        var result = await groupService.AddAvatarAsync(UserId, groupId, file);
        
        return result.IsSuccess
            ? Results.Ok(result.Value)
            : result.ToProblemDetails();
    }

    [HttpDelete("{groupId:guid}/avatar/{resourceId:guid}")]
    public async Task<IResult> RemoveAvatar(Guid groupId, Guid resourceId)
    {
        var result = await groupService.RemoveAvatarAsync(UserId, groupId, resourceId);
        
        return result.IsSuccess
            ? Results.Ok()
            : result.ToProblemDetails();
    }

    [HttpGet("{groupId:guid}")]
    public async Task<IResult> GetGroup(Guid groupId)
    {
        var result = await groupService.GetGroupAsync(UserId, groupId);
        
        return result.IsSuccess
            ? Results.Ok(result.Value)
            : result.ToProblemDetails();
    }
    
    [HttpDelete("{groupId:guid}")]
    public async Task<IResult> RemoveGroup(Guid groupId)
    {
        var result = await groupService.RemoveGroupAsync(UserId, groupId);
        
        return result.IsSuccess
            ? Results.Ok()
            : result.ToProblemDetails();
    }

    [HttpPut("{groupId:guid}/permission")]
    public async Task<IResult> UpdatePermissions(Guid groupId, PermissionsDto dto)
    {
        var result = await groupService.UpdatePermissionsAsync(UserId, groupId, dto);
        
        return result.IsSuccess
            ? Results.Ok()
            : result.ToProblemDetails();
    }

    [HttpDelete("{groupId:guid}/leave")]
    public async Task<IResult> LeaveGroup(Guid groupId)
    {
        var result = await groupService.LeaveGroupAsync(UserId, groupId);
        
        return result.IsSuccess
            ? Results.Ok()
            : result.ToProblemDetails();
    }

    [HttpPost("{groupId:guid}/join")]
    public async Task<IResult> JoinGroup(Guid groupId)
    {
        var result = await groupService.JoinGroupAsync(UserId, groupId);

        return result.IsSuccess
            ? Results.Ok()
            : result.ToProblemDetails();
    }
    
    [HttpGet("{groupId:guid}/messages")]
    public async Task<IResult> GetAllMessages(Guid groupId, [FromQuery] MessageParameters parameters)
    {
        var result = await groupService.GetAllMessagesAsync(UserId, groupId, parameters);

        if (!result.IsSuccess)
            return result.ToProblemDetails();

        Response.Headers["X-PagedData"] = JsonConvert.SerializeObject(result.Value!.PagedData);

        return Results.Ok(result.Value);
    }
    
    [HttpPost("{groupId:guid}/message")]
    public async Task<IResult> AddMessage(Guid groupId, NewMessageDto dto)
    {
        var result = await groupService.AddMessageAsync(
            UserId,
            groupId,
            dto);

        return result.IsSuccess
            ? Results.Ok(result.Value)
            : result.ToProblemDetails();
    }

    [HttpDelete("{groupId:guid}/message/{messageId:guid}")]
    public async Task<IResult> RemoveMessage(Guid groupId, Guid messageId)
    {
        var result = await groupService.RemoveMessageAsync(
            UserId,
            groupId,
            messageId);

        return result.IsSuccess
            ? Results.Ok()
            : result.ToProblemDetails();
    }
    
    [HttpDelete("{groupId:guid}/message/self/{messageId:guid}")]
    public async Task<IResult> RemoveMessageForSelf(Guid groupId, Guid messageId)
    {
        var result = await groupService.RemoveMessageForSelfAsync(
            UserId,
            groupId,
            messageId);

        return result.IsSuccess
            ? Results.Ok()
            : result.ToProblemDetails();
    }
    
    [HttpPost("{groupId:guid}/message/{messageId:guid}/reaction")]
    public async Task<IResult> AddReaction(Guid groupId, Guid messageId, ReactionType type)
    {
        var result = await groupService.AddReactionAsync(
            UserId,
            groupId,
            messageId,
            type);

        return result.IsSuccess
            ? Results.Ok(result.Value)
            : result.ToProblemDetails();
    }

    [HttpDelete("{groupId:guid}/message/{messageId:guid}/reaction/{reactionId:guid}")]
    public async Task<IResult> RemoveReaction(Guid groupId, Guid messageId, Guid reactionId)
    {
        var result = await groupService.RemoveReactionAsync(
            UserId,
            groupId,
            messageId,
            reactionId);

        return result.IsSuccess
            ? Results.Ok()
            : result.ToProblemDetails();
    }
    
    [HttpGet("{groupId:guid}/bans")]
    public async Task<IResult> GetBannedMember(Guid groupId, [FromQuery] MemberParameters parameters)
    {
        var result = await groupService.GetBannedMemberAsync(UserId, groupId, parameters);

        if (!result.IsSuccess)
            return result.ToProblemDetails();

        Response.Headers["X-PagedData"] = JsonConvert.SerializeObject(result.Value!.PagedData);

        return Results.Ok(result.Value);
    }
}