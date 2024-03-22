using AutoMapper;
using ChatApp.Server.Application.Core;
using ChatApp.Server.Application.Core.Abstractions;
using ChatApp.Server.Application.Groups.Dtos;
using ChatApp.Server.Application.Shared.Dtos;
using ChatApp.Server.Domain.Core;
using ChatApp.Server.Domain.Core.Abstractions.Results;
using ChatApp.Server.Domain.Groups;
using ChatApp.Server.Domain.Groups.Errors;
using ChatApp.Server.Domain.Groups.Repositories;
using ChatApp.Server.Domain.Resources;
using ChatApp.Server.Domain.Resources.Repositories;

namespace ChatApp.Server.Application.Groups;

public sealed class GroupService(
    IGroupRepository groupRepository,
    IGroupMembershipRepository groupMembershipRepository,
    IGroupRoleRepository groupRoleRepository,
    IGroupAvatarRepository groupAvatarRepository,
    IResourceRepository resourceRepository,
    IGroupBanRepository groupBanRepository,
    IMapper mapper,
    IGroupRequestRepository groupRequestRepository,
    IUnitOfWork unitOfWork)
    : IGroupService
{
    public async Task<Result<GroupDto>> GetGroupAsync(Guid userId, Guid groupId)
    {
        var group = await groupRepository.GetByIdAsync(groupId, true);

        if (group is null)
            return Result<GroupDto>.Failure(GroupErrors.NotFound);

        var membership = await groupMembershipRepository.GetByIdsAsync(group.Id, userId);

        return membership is null
            ? Result<GroupDto>.Failure(GroupMembershipErrors.NotFound)
            : Result<GroupDto>.Success(mapper.Map<GroupDto>(group));
    }

    public async Task<Result<Guid>> CreateGroupAsync(Guid userId, NewGroupDto dto)
    {
        if (dto.Avatar is not null && !AvatarValidator.IsValid(dto.Avatar.Extension))
            return Result<Guid>.Failure(GroupAvatarErrors.Invalid);

        var group = new Group(dto.Name);

        var membership = new GroupMembership(group.Id, userId);

        var role = GroupRole.CreateOwner(group.Id);

        var transaction = await unitOfWork.BeginTransactionAsync();

        try
        {
            await groupRepository.AddAsync(group);
            await groupMembershipRepository.AddAsync(membership);
            await groupRoleRepository.AddAsync(role);

            membership.RoleId = role.Id;

            if (dto.Avatar is not null)
            {
                var resource = mapper.Map<Resource>(dto.Avatar);
                var avatar = new GroupAvatar(group.Id, resource.Id);

                await resourceRepository.AddAsync(resource);
                await groupAvatarRepository.AddAsync(avatar);
            }

            await unitOfWork.SaveChangesAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();

            return Result<Guid>.Failure(GroupErrors.CreateError);
        }

        await transaction.CommitAsync();

        return Result<Guid>.Success(group.Id);
    }

    public async Task<Result> RemoveGroupAsync(Guid userId, Guid groupId)
    {
        var group = await groupRepository.GetByIdAsync(groupId, includeAvatarResource: true);

        if (group is null)
            return Result.Failure(GroupErrors.NotFound);

        var membership = await groupMembershipRepository.GetByIdsAsync(group.Id, userId, true);

        if (membership is null)
            return Result.Failure(GroupMembershipErrors.NotFound);

        if (membership.Role is null || !membership.Role.IsOwner)
            return Result.Failure(GroupRoleErrors.NotEnoughRights);

        var transaction = await unitOfWork.BeginTransactionAsync();

        try
        {
            groupRepository.Remove(group);

            if (group.Avatars.Count > 0)
            {
                var resources = group.Avatars.Select(avatar => avatar.Resource);

                resourceRepository.RemoveRange(resources);
            }

            await unitOfWork.SaveChangesAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();

            return Result.Failure(GroupErrors.RemoveError);
        }

        await transaction.CommitAsync();

        return Result.Success();
    }

    public async Task<Result<GroupInfoDto>> UpdateInfoAsync(Guid userId, Guid groupId, GroupInfoDto dto)
    {
        var group = await groupRepository.GetByIdAsync(groupId);

        if (group is null)
            return Result<GroupInfoDto>.Failure(GroupErrors.NotFound);

        var membership = await groupMembershipRepository.GetByIdsAsync(group.Id, userId, true);

        if (membership is null)
            return Result<GroupInfoDto>.Failure(GroupMembershipErrors.NotFound);

        if (membership.Role is null or { IsOwner: false, AllowChangeGroupInfo: false, AllowManageSecurity: false })
            return Result<GroupInfoDto>.Failure(GroupRoleErrors.NotEnoughRights);

        mapper.Map(dto, group);

        try
        {
            groupRepository.Update(group);
            await unitOfWork.SaveChangesAsync();
        }
        catch (Exception)
        {
            return Result<GroupInfoDto>.Failure(GroupErrors.UpdateError);
        }

        return Result<GroupInfoDto>.Success(mapper.Map<GroupInfoDto>(group));
    }

    public async Task<Result<AvatarDto>> AddAvatarAsync(Guid userId, Guid groupId, NewResourceDto dto)
    {
        if (!AvatarValidator.IsValid(dto.Extension))
            return Result<AvatarDto>.Failure(GroupAvatarErrors.Invalid);

        var group = await groupRepository.GetByIdAsync(groupId);

        if (group is null)
            return Result<AvatarDto>.Failure(GroupErrors.NotFound);

        var membership = await groupMembershipRepository.GetByIdsAsync(group.Id, userId, true);

        if (membership is null)
            return Result<AvatarDto>.Failure(GroupMembershipErrors.NotFound);

        if (membership.Role is null or { IsOwner: false, AllowChangeGroupInfo: false, AllowManageSecurity: false })
            return Result<AvatarDto>.Failure(GroupRoleErrors.NotEnoughRights);

        var resource = mapper.Map<Resource>(dto);
        var avatar = new GroupAvatar(group.Id, resource.Id);

        var transaction = await unitOfWork.BeginTransactionAsync();

        try
        {
            await resourceRepository.AddAsync(resource);
            await groupAvatarRepository.AddAsync(avatar);
            await unitOfWork.SaveChangesAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();

            return Result<AvatarDto>.Failure(GroupAvatarErrors.AddError);
        }

        await transaction.CommitAsync();

        return Result<AvatarDto>.Success(mapper.Map<AvatarDto>(avatar));
    }

    public async Task<Result> RemoveAvatarAsync(Guid userId, Guid groupId, Guid resourceId)
    {
        var group = await groupRepository.GetByIdAsync(groupId);

        if (group is null)
            return Result.Failure(GroupErrors.NotFound);

        var avatar = await groupAvatarRepository.GetByIdsAsync(group.Id, resourceId, true);

        if (avatar is null)
            return Result.Failure(GroupAvatarErrors.NotFound);

        var membership = await groupMembershipRepository.GetByIdsAsync(group.Id, userId, true);

        if (membership is null)
            return Result.Failure(GroupMembershipErrors.NotFound);

        if (membership.Role is null or { IsOwner: false, AllowChangeGroupInfo: false, AllowManageSecurity: false })
            return Result.Failure(GroupRoleErrors.NotEnoughRights);

        try
        {
            resourceRepository.Remove(avatar.Resource);
            await unitOfWork.SaveChangesAsync();
        }
        catch (Exception)
        {
            return Result.Failure(GroupAvatarErrors.RemoveError);
        }

        return Result.Success();
    }

    public async Task<Result> JoinGroupAsync(Guid userId, Guid groupId)
    {
        var group = await groupRepository.GetByIdAsync(groupId);

        if (group is null)
            return Result.Failure(GroupErrors.NotFound);

        var membership = await groupMembershipRepository.GetByIdsAsync(group.Id, userId);

        if (membership is not null)
            return Result.Failure(GroupMembershipErrors.AlreadyMember);

        var ban = await groupBanRepository.GetByIdsAsync(group.Id, userId);

        if (ban is not null)
            return Result.Failure(GroupBanErrors.Banned);

        if (group.IsPublic)
        {
            membership = new GroupMembership(group.Id, userId);

            try
            {
                await groupMembershipRepository.AddAsync(membership);
                await unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                return Result.Failure(GroupMembershipErrors.JoinError);
            }

            return Result.Success();
        }

        var request = await groupRequestRepository.GetByIdsAsync(group.Id, userId);

        if (request is not null)
            return Result.Failure(GroupRequestErrors.AlreadyExist);

        request = new GroupRequest(group.Id, userId);

        try
        {
            await groupRequestRepository.AddAsync(request);
            await unitOfWork.SaveChangesAsync();
        }
        catch (Exception)
        {
            return Result.Failure(GroupRequestErrors.AddError);
        }

        return Result.Success();
    }

    public async Task<Result> LeaveGroupAsync(Guid userId, Guid groupId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> ApproveJoinRequestAsync(Guid userId, Guid groupId, Guid requestUserId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> DeclineJoinRequestAsync(Guid userId, Guid groupId, Guid requestUserId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> AddUserAsync(Guid userId, Guid groupId, Guid userToAddId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> RemoveUserAsync(Guid userId, Guid groupId, Guid userToRemoveId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> BanUserAsync(Guid userId, Guid groupId, Guid userToBanId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> UnbanUserAsync(Guid userId, Guid groupId, Guid userToUnbanId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<MessageDto>> AddMessageAsync(Guid userId, Guid groupId, NewMessageDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<Guid>> RemoveMessageAsync(Guid userId, Guid groupId, Guid messageId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> RemoveMessageForSelfAsync(Guid userId, Guid groupId, Guid messageId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<ReactionDto>> AddReactionAsync(Guid userId, Guid groupId, Guid messageId,
        ReactionType type)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<Guid>> RemoveReactionAsync(Guid userId, Guid groupId, Guid messageId, Guid reactionId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<List<GroupRoleDto>>> GetRolesAsync(Guid userId, Guid groupId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<GroupRoleDto>> AddRoleAsync(Guid userId, Guid groupId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> RemoveRoleAsync(Guid userId, Guid groupId, Guid roleId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<GroupRoleDto>> UpdateRoleAsync(Guid userId, Guid groupId, UpdateGroupRoleDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> AddMemberToRole(Guid userId, Guid groupId, Guid memberToAddId, Guid roleId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> RemoveMemberFromRole(Guid userId, Guid groupId, Guid memberToRemoveId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<GroupPermissionsDto>> GetPermissionsAsync(Guid userId, Guid groupId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<GroupPermissionsDto>> UpdatePermissionsAsync(Guid userId, Guid groupId,
        GroupPermissionsDto dto)
    {
        throw new NotImplementedException();
    }
}