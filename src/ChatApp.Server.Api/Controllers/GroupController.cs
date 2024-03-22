using AutoMapper;
using ChatApp.Server.Api.Core.Abstractions;
using ChatApp.Server.Api.Core.Extensions;
using ChatApp.Server.Api.Requests;
using ChatApp.Server.Application.Groups;
using ChatApp.Server.Application.Groups.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Server.Api.Controllers;

[Route("api/group")]
public sealed class GroupController(
    IGroupService groupService,
    IMapper mapper)
    : ApiController
{
    [HttpGet("{groupId:guid}")]
    public async Task<IResult> GetGroup(Guid groupId)
    {
        var result = await groupService.GetGroupAsync(UserId, groupId);

        return result.IsSuccess
            ? Results.Ok(result.Value)
            : result.ToProblemDetails();
    }

    [HttpPost]
    public async Task<IResult> CreateGroup([FromForm] NewGroupRequest request)
    {
        var result = await groupService.CreateGroupAsync(
            UserId,
            mapper.Map<NewGroupDto>(request));

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

    [HttpPut("{groupId:guid}")]
    public async Task<IResult> UpdateInfo(Guid groupId, GroupInfoDto dto)
    {
        var result = await groupService.UpdateInfoAsync(UserId, groupId, dto);

        return result.IsSuccess
            ? Results.Ok(result.Value)
            : result.ToProblemDetails();
    }

    [HttpPost("{groupId:guid}/avatar")]
    public async Task<IResult> AddAvatar(Guid groupId, IFormFile file)
    {
        var result = await groupService.AddAvatarAsync(UserId, groupId, file.ToNewResourceDto());

        return result.IsSuccess
            ? Results.Ok(result.Value)
            : result.ToProblemDetails();
    }
    
    [HttpDelete("{groupId:guid}/avatar/{resourceId:guid}")]
    public async Task<IResult> AddAvatar(Guid groupId, Guid resourceId)
    {
        var result = await groupService.RemoveAvatarAsync(UserId, groupId, resourceId);

        return result.IsSuccess
            ? Results.Ok()
            : result.ToProblemDetails();
    }

    [HttpPost("join/{groupId:guid}")]
    public async Task<IResult> JoinGroup(Guid groupId)
    {
        var result = await groupService.JoinGroupAsync(UserId, groupId);

        return result.IsSuccess
            ? Results.Ok()
            : result.ToProblemDetails();
    }
}