using ChatApp.Server.Application.Groups.Dtos;
using ChatApp.Server.Application.Shared.Dtos;
using ChatApp.Server.Domain.Core;
using ChatApp.Server.Domain.Core.Abstractions.Results;

namespace ChatApp.Server.Application.Groups;

public interface IGroupService
{
    // Group Management
    
    Task<Result<GroupDto>> GetGroupAsync(Guid userId, Guid groupId);
    
    Task<Result<Guid>> CreateGroupAsync(Guid userId, NewGroupDto dto);
    
    Task<Result> RemoveGroupAsync(Guid userId, Guid groupId);
    
    Task<Result<GroupInfoDto>> UpdateInfoAsync(Guid userId, Guid groupId, GroupInfoDto dto);
    
    Task<Result<AvatarDto>> AddAvatarAsync(Guid userId, Guid groupId, NewResourceDto dto);

    Task<Result> RemoveAvatarAsync(Guid userId, Guid groupId, Guid resourceId);
    
    // Membership Management

    Task<Result> JoinGroupAsync(Guid userId, Guid groupId);
    
    Task<Result> LeaveGroupAsync(Guid userId, Guid groupId);

    Task<Result> ApproveJoinRequestAsync(Guid userId, Guid groupId, Guid requestUserId);
    
    Task<Result> DeclineJoinRequestAsync(Guid userId, Guid groupId, Guid requestUserId);
    
    // User Management within a Group
    
    Task<Result> AddUserAsync(Guid userId, Guid groupId, Guid userToAddId);

    Task<Result> RemoveUserAsync(Guid userId, Guid groupId, Guid userToRemoveId);
    
    Task<Result> BanUserAsync(Guid userId, Guid groupId, Guid userToBanId);

    Task<Result> UnbanUserAsync(Guid userId, Guid groupId, Guid userToUnbanId);
    
    // Messaging

    Task<Result<MessageDto>> AddMessageAsync(Guid userId, Guid groupId, NewMessageDto dto);
    
    Task<Result<Guid>> RemoveMessageAsync(Guid userId, Guid groupId, Guid messageId);
    
    Task<Result> RemoveMessageForSelfAsync(Guid userId, Guid groupId, Guid messageId);
    
    Task<Result<ReactionDto>> AddReactionAsync(Guid userId, Guid groupId, Guid messageId, ReactionType type);

    Task<Result<Guid>> RemoveReactionAsync(Guid userId, Guid groupId, Guid messageId, Guid reactionId);
    
    // Role Management

    Task<Result<List<GroupRoleDto>>> GetRolesAsync(Guid userId, Guid groupId);

    Task<Result<GroupRoleDto>> AddRoleAsync(Guid userId, Guid groupId);

    Task<Result> RemoveRoleAsync(Guid userId, Guid groupId, Guid roleId);

    Task<Result<GroupRoleDto>> UpdateRoleAsync(Guid userId, Guid groupId, UpdateGroupRoleDto dto);
    
    // Role Membership Management
    
    Task<Result> AddMemberToRole(Guid userId, Guid groupId, Guid memberToAddId, Guid roleId);

    Task<Result> RemoveMemberFromRole(Guid userId, Guid groupId, Guid memberToRemoveId);
    
    // Permissions Management
    
    Task<Result<GroupPermissionsDto>> GetPermissionsAsync(Guid userId, Guid groupId);
    
    Task<Result<GroupPermissionsDto>> UpdatePermissionsAsync(Guid userId, Guid groupId, GroupPermissionsDto dto);
}