using ChatApp.Server.Application.Groups;
using ChatApp.Server.Application.Groups.Dtos;
using ChatApp.Server.Presentation.Core.Abstractions;
using ChatApp.Server.Presentation.Core.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
}