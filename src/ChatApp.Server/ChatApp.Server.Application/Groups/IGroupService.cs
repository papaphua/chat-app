using ChatApp.Server.Application.Groups.Dtos;
using ChatApp.Server.Application.Shared.Dtos;
using ChatApp.Server.Domain.Core;
using ChatApp.Server.Domain.Core.Abstractions.Paging;
using ChatApp.Server.Domain.Core.Abstractions.Results;
using ChatApp.Server.Domain.Shared;
using Microsoft.AspNetCore.Http;

namespace ChatApp.Server.Application.Groups;

public interface IGroupService
{
    //Group actions

    Task<Result<Guid>> CreateGroupAsync(Guid userId, NewGroupDto dto);

    Task<Result> UpdateGroupInfoAsync(Guid userId, Guid groupId, GroupInfoDto dto);

    Task<Result<AvatarDto>> AddAvatarAsync(Guid userId, IFormFile file);

    Task<Result> RemoveAvatarAsync(Guid userId, Guid resourceId);

    Task<Result<GroupDto>> GetGroupAsync(Guid userId, Guid groupId);

    Task<Result> RemoveGroupAsync(Guid userId, Guid groupId);

    //Group join actions

    Task<Result> LeaveGroupAsync(Guid userId, Guid groupId);

    Task<Result> JoinGroupAsync(Guid userId, Guid groupId);

    //Message actions

    Task<Result<PagedList<MessageDto>>> GetAllMessagesAsync(Guid userId, Guid groupId, MessageParameters parameters);

    Task<Result<MessageDto>> AddMessageAsync(Guid userId, Guid groupId, NewMessageDto dto);

    Task<Result> RemoveMessageAsync(Guid userId, Guid groupId, Guid messageId);

    Task<Result> RemoveMessageForSelfAsync(Guid userId, Guid groupId, Guid messageId);

    Task<Result<ReactionDto>> AddReactionAsync(Guid userId, Guid groupId, Guid messageId, ReactionType type);

    Task<Result> RemoveReactionAsync(Guid userId, Guid groupId, Guid messageId, Guid reactionId);

    //Ban actions

    Task<Result<PagedList<BanDto>>> GetBannedMemberAsync(Guid userId, Guid groupId);
    
    Task<Result> BanMemberAsync(Guid userId, Guid groupId, Guid memberToBanId);

    Task<Result> UnbanMemberAsync(Guid userId, Guid groupId, Guid memberToUnbanId);

    //User add actions

    Task<Result<PagedList<GroupInvitationDto>>> GetInvitationsAsync(Guid userId, Guid groupId);
    
    Task<Result<GroupInvitationDto>> CreateInvitationLinkAsync(Guid userId, Guid groupId);

    Task<Result> RemoveInvitationLinkAsync(Guid userId, Guid groupId, Guid invitationId);

    Task<Result<PagedList<GroupMemberDto>>> GetMemberAsync(Guid userId, Guid groupId);
    
    Task<Result> AddMemberAsync(Guid userId, Guid groupId, Guid memberToAddId);

    Task<Result> RemoveMemberAsync(Guid userId, Guid groupId, Guid memberToAddId);

    //Role actions

    Task<Result<PagedList<RoleDto>>> GetRolesAsync(Guid userId, Guid groupId);
    
    Task<Result<RoleDto>> CreateRoleAsync(Guid userId, Guid groupId, NewRoleDto dto);

    Task<Result> RemoveRoleAsync(Guid userId, Guid groupId, Guid roleId);

    Task<Result> PromoteMemberAsync(Guid userId, Guid groupId, Guid memberToPromoteId, Guid roleId);

    Task<Result> DemoteMemberAsync(Guid userId, Guid groupId, Guid memberToDemoteId);
}