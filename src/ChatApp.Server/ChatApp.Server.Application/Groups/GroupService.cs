using ChatApp.Server.Application.Groups.Dtos;
using ChatApp.Server.Application.Shared.Dtos;
using ChatApp.Server.Domain.Core;
using ChatApp.Server.Domain.Core.Abstractions.Paging;
using ChatApp.Server.Domain.Core.Abstractions.Results;
using ChatApp.Server.Domain.Shared;
using Microsoft.AspNetCore.Http;

namespace ChatApp.Server.Application.Groups;

public sealed class GroupService : IGroupService
{
    public async Task<Result<Guid>> CreateGroupAsync(Guid userId, NewGroupDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> UpdateGroupInfoAsync(Guid userId, Guid groupId, GroupInfoDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<AvatarDto>> AddAvatarAsync(Guid userId, IFormFile file)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> RemoveAvatarAsync(Guid userId, Guid resourceId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<GroupDto>> GetGroupAsync(Guid userId, Guid groupId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> RemoveGroupAsync(Guid userId, Guid groupId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> LeaveGroupAsync(Guid userId, Guid groupId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> JoinGroupAsync(Guid userId, Guid groupId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<PagedList<MessageDto>>> GetAllMessagesAsync(Guid userId, Guid groupId, MessageParameters parameters)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<MessageDto>> AddMessageAsync(Guid userId, Guid groupId, NewMessageDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> RemoveMessageAsync(Guid userId, Guid groupId, Guid messageId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> RemoveMessageForSelfAsync(Guid userId, Guid groupId, Guid messageId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<ReactionDto>> AddReactionAsync(Guid userId, Guid groupId, Guid messageId, ReactionType type)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> RemoveReactionAsync(Guid userId, Guid groupId, Guid messageId, Guid reactionId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<PagedList<BanDto>>> GetBannedMemberAsync(Guid userId, Guid groupId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> BanMemberAsync(Guid userId, Guid groupId, Guid memberToBanId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> UnbanMemberAsync(Guid userId, Guid groupId, Guid memberToUnbanId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<PagedList<GroupInvitationDto>>> GetInvitationsAsync(Guid userId, Guid groupId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<GroupInvitationDto>> CreateInvitationLinkAsync(Guid userId, Guid groupId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> RemoveInvitationLinkAsync(Guid userId, Guid groupId, Guid invitationId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<PagedList<GroupMemberDto>>> GetMemberAsync(Guid userId, Guid groupId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> AddMemberAsync(Guid userId, Guid groupId, Guid memberToAddId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> RemoveMemberAsync(Guid userId, Guid groupId, Guid memberToAddId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<PagedList<RoleDto>>> GetRolesAsync(Guid userId, Guid groupId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<RoleDto>> CreateRoleAsync(Guid userId, Guid groupId, NewRoleDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> RemoveRoleAsync(Guid userId, Guid groupId, Guid roleId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> PromoteMemberAsync(Guid userId, Guid groupId, Guid memberToPromoteId, Guid roleId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> DemoteMemberAsync(Guid userId, Guid groupId, Guid memberToDemoteId)
    {
        throw new NotImplementedException();
    }
}