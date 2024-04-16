using AutoMapper;
using ChatApp.Server.Application.Core.Abstractions;
using ChatApp.Server.Application.Core.Extensions;
using ChatApp.Server.Application.Groups.Dtos;
using ChatApp.Server.Application.Shared.Dtos;
using ChatApp.Server.Domain.Core;
using ChatApp.Server.Domain.Core.Abstractions.Paging;
using ChatApp.Server.Domain.Core.Abstractions.Results;
using ChatApp.Server.Domain.Groups;
using ChatApp.Server.Domain.Groups.Errors;
using ChatApp.Server.Domain.Groups.Repositories;
using ChatApp.Server.Domain.Resources;
using ChatApp.Server.Domain.Resources.Repositories;
using ChatApp.Server.Domain.Shared;
using ChatApp.Server.Domain.Users.Errors;
using ChatApp.Server.Domain.Users.Repositories;
using Microsoft.AspNetCore.Http;
using Group = ChatApp.Server.Domain.Groups.Group;

namespace ChatApp.Server.Application.Groups;

public sealed class GroupService(
    IGroupRepository groupRepository,
    IGroupMembershipRepository groupMembershipRepository,
    IResourceRepository resourceRepository,
    IGroupAvatarRepository groupAvatarRepository,
    IGroupBanRepository groupBanRepository,
    IGroupMessageRepository groupMessageRepository,
    IGroupAttachmentRepository groupAttachmentRepository,
    IGroupReactionRepository groupReactionRepository,
    IGroupDeletionRepository groupDeletionRepository,
    IGroupInvitationRepository groupInvitationRepository,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IGroupService
{
    public async Task<Result<Guid>> CreateGroupAsync(Guid userId, NewGroupDto dto)
    {
        var group = new Group(dto.Name, userId);
        var membership = new GroupMembership(group.Id, userId);

        try
        {
            await groupRepository.AddAsync(group);
            await groupMembershipRepository.AddAsync(membership);
            await unitOfWork.SaveChangesAsync();
        }
        catch (Exception)
        {
            return Result<Guid>.Failure(GroupErrors.CreateError);
        }

        return Result<Guid>.Success(group.Id);
    }

    public async Task<Result> UpdateGroupInfoAsync(Guid userId, Guid groupId, GroupInfoDto dto)
    {
        var membership = await groupMembershipRepository.GetByGroupIdAndMemberIdAsync(groupId, userId,
            true, includeRole: true);

        if (membership is null)
            return Result.Failure(GroupMembershipErrors.NotFound);

        if (membership.Group.OwnerId != userId
            && !(membership.Role is not null && membership.Role.AllowChangeGroupInfo))
            return Result.Failure(GroupRoleErrors.NotAllowed);

        mapper.Map(dto, membership.Group);

        try
        {
            groupRepository.Update(membership.Group);
            await unitOfWork.SaveChangesAsync();
        }
        catch (Exception)
        {
            return Result.Failure(GroupErrors.UpdateError);
        }

        return Result.Success();
    }

    public async Task<Result<AvatarDto>> AddAvatarAsync(Guid userId, Guid groupId, IFormFile file)
    {
        var resource = file.ToResource();

        if (!FileExtensionMapping.IsImage(resource))
            return Result<AvatarDto>.Failure(GroupAvatarErrors.Invalid);

        var membership = await groupMembershipRepository.GetByGroupIdAndMemberIdAsync(groupId, userId,
            true, includeRole: true);

        if (membership is null)
            return Result<AvatarDto>.Failure(GroupMembershipErrors.NotFound);

        if (membership.Group.OwnerId != userId
            && !(membership.Role is not null && membership.Role.AllowChangeGroupInfo))
            return Result<AvatarDto>.Failure(GroupRoleErrors.NotAllowed);

        var avatar = new GroupAvatar(groupId, resource.Id);

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

            return Result<AvatarDto>.Failure(GroupAvatarErrors.CreateError);
        }

        await transaction.CommitAsync();

        return Result<AvatarDto>.Success(mapper.Map<AvatarDto>(avatar));
    }

    public async Task<Result> RemoveAvatarAsync(Guid userId, Guid groupId, Guid resourceId)
    {
        var membership = await groupMembershipRepository.GetByGroupIdAndMemberIdAsync(groupId, userId,
            true, includeRole: true);

        if (membership is null)
            return Result.Failure(GroupMembershipErrors.NotFound);

        if (membership.Group.OwnerId != userId
            && !(membership.Role is not null && membership.Role.AllowChangeGroupInfo))
            return Result<AvatarDto>.Failure(GroupRoleErrors.NotAllowed);

        var resource = await resourceRepository.GetByIdAsync(resourceId);

        if (resource is null)
            return Result.Failure(GroupAvatarErrors.NotFound);

        try
        {
            resourceRepository.Remove(resource);
            await unitOfWork.SaveChangesAsync();
        }
        catch (Exception)
        {
            return Result.Failure(GroupAvatarErrors.RemoveError);
        }

        return Result.Success();
    }

    public async Task<Result<GroupDto>> GetGroupAsync(Guid userId, Guid groupId)
    {
        var membership = await groupMembershipRepository.GetByGroupIdAndMemberIdAsync(groupId, userId,
            includeGroupWithAvatars: true);

        return membership is null
            ? Result<GroupDto>.Failure(GroupMembershipErrors.NotFound)
            : Result<GroupDto>.Success(mapper.Map<GroupDto>(membership.Group));
    }

    public async Task<Result> RemoveGroupAsync(Guid userId, Guid groupId)
    {
        var membership = await groupMembershipRepository.GetByGroupIdAndMemberIdAsync(groupId, userId,
            true);

        if (membership is null)
            return Result.Failure(GroupMembershipErrors.NotFound);

        if (membership.Group.OwnerId != userId)
            return Result.Failure(GroupRoleErrors.NotAllowed);

        try
        {
            groupRepository.Remove(membership.Group);
            await unitOfWork.SaveChangesAsync();
        }
        catch (Exception)
        {
            return Result.Failure(GroupErrors.RemoveError);
        }

        return Result.Success();
    }

    public async Task<Result> UpdatePermissionsAsync(Guid userId, Guid groupId, PermissionsDto dto)
    {
        var membership = await groupMembershipRepository.GetByGroupIdAndMemberIdAsync(groupId, userId,
            true);

        if (membership is null)
            return Result.Failure(GroupMembershipErrors.NotFound);

        if (membership.Group.OwnerId != userId)
            return Result.Failure(GroupRoleErrors.NotAllowed);

        mapper.Map(dto, membership.Group);

        try
        {
            groupRepository.Update(membership.Group);
            await unitOfWork.SaveChangesAsync();
        }
        catch (Exception)
        {
            return Result.Failure(GroupErrors.UpdateError);
        }

        return Result.Success();
    }

    public async Task<Result> LeaveGroupAsync(Guid userId, Guid groupId)
    {
        var membership = await groupMembershipRepository.GetByGroupIdAndMemberIdAsync(groupId, userId,
            true);

        if (membership is null)
            return Result.Failure(GroupMembershipErrors.NotFound);

        var transaction = await unitOfWork.BeginTransactionAsync();

        try
        {
            groupMembershipRepository.Remove(membership);

            if (membership.Group.OwnerId == userId)
            {
                membership.Group.OwnerId = null;
                groupRepository.Update(membership.Group);
            }

            await unitOfWork.SaveChangesAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();

            return Result.Failure(GroupMembershipErrors.RemoveError);
        }

        await transaction.CommitAsync();

        return Result.Success();
    }

    public async Task<Result> JoinGroupAsync(Guid userId, Guid groupId)
    {
        var group = await groupRepository.GetByIdAsync(groupId);

        if (group is null)
            return Result.Failure(GroupErrors.NotFound);

        var membership = await groupMembershipRepository.GetByGroupIdAndMemberIdAsync(groupId, userId);

        if (membership is not null)
            return Result.Failure(GroupMembershipErrors.AlreadyExist);

        var ban = await groupBanRepository.GetByGroupIdAndMemberIdAsync(groupId, userId);

        if (ban is not null)
            return Result.Failure(GroupBanErrors.Banned);

        membership = new GroupMembership(group.Id, userId);

        try
        {
            await groupMembershipRepository.AddAsync(membership);
            await unitOfWork.SaveChangesAsync();
        }
        catch (Exception)
        {
            return Result.Failure(GroupMembershipErrors.CreateError);
        }

        return Result.Success();
    }

    public async Task<Result<PagedList<MessageDto>>> GetAllMessagesAsync(Guid userId, Guid groupId,
        MessageParameters parameters)
    {
        var messages = await groupMessageRepository.GetPagedByGroupIdAndUserIdAsync(groupId,
            userId, parameters);

        return Result<PagedList<MessageDto>>.Success(
            messages.Select(mapper.Map<MessageDto>).ToPagedList(messages));
    }

    public async Task<Result<MessageDto>> AddMessageAsync(Guid userId, Guid groupId, NewMessageDto dto)
    {
        var membership = await groupMembershipRepository.GetByGroupIdAndMemberIdAsync(groupId, userId,
            true);

        if (membership is null)
            return Result<MessageDto>.Failure(GroupMembershipErrors.NotFound);

        if ((dto.Content is not null && !membership.Group.AllowSendTextMessages)
            || dto.Attachments?.Count > 0 && !membership.Group.AllowSendFiles)
            return Result<MessageDto>.Failure(GroupRoleErrors.NotAllowed);

        var message = new GroupMessage(groupId, userId) { Content = dto.Content };

        var hasAttachments = dto.Attachments?.Count > 0;
        List<Resource> resources = [];
        List<GroupAttachment> attachments = [];

        if (hasAttachments)
        {
            resources = dto.Attachments.ToResources();
            attachments = resources.Select(resource => new GroupAttachment(message.Id, resource.Id)).ToList();
        }

        var transaction = await unitOfWork.BeginTransactionAsync();

        try
        {
            await groupMessageRepository.AddAsync(message);

            if (hasAttachments)
            {
                await resourceRepository.AddRangeAsync(resources);
                await groupAttachmentRepository.AddRangeAsync(attachments);
            }

            await unitOfWork.SaveChangesAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();

            return Result<MessageDto>.Failure(GroupMessageErrors.CreateError);
        }

        await transaction.CommitAsync();

        return Result<MessageDto>.Success(mapper.Map<MessageDto>(message));
    }

    public async Task<Result> RemoveMessageAsync(Guid userId, Guid groupId, Guid messageId)
    {
        var membership = await groupMembershipRepository.GetByGroupIdAndMemberIdAsync(groupId, userId,
            true, includeRole: true);

        if (membership is null)
            return Result<MessageDto>.Failure(GroupMembershipErrors.NotFound);

        var message = await groupMessageRepository.GetByIdAsync(messageId);

        var attachments = await groupAttachmentRepository.GetByMessageIdAsync(messageId, true);
        var resources = attachments.Select(attachment => attachment.Resource).ToList();

        if (message is null
            || message.GroupId != groupId)
            return Result.Failure(GroupMessageErrors.NotFound);

        if (message.SenderId != userId
            && membership.Role is not null && !membership.Role.AllowDeleteMessage)
            return Result.Failure(GroupRoleErrors.NotAllowed);

        var transaction = await unitOfWork.BeginTransactionAsync();

        try
        {
            if (resources.Count > 0)
                resourceRepository.RemoveRange(resources);

            groupMessageRepository.Remove(message);

            await unitOfWork.SaveChangesAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();

            return Result.Failure(GroupMessageErrors.RemoveError);
        }

        await transaction.CommitAsync();

        return Result.Success();
    }

    public async Task<Result> RemoveMessageForSelfAsync(Guid userId, Guid groupId, Guid messageId)
    {
        var membership = await groupMembershipRepository.GetByGroupIdAndMemberIdAsync(groupId, userId,
            true, includeRole: true);

        if (membership is null)
            return Result<MessageDto>.Failure(GroupMembershipErrors.NotFound);

        var message = await groupMessageRepository.GetByIdAsync(messageId);

        if (message is null
            || message.GroupId != groupId)
            return Result.Failure(GroupMessageErrors.NotFound);

        if (message.SenderId != userId)
            return Result.Failure(GroupRoleErrors.NotAllowed);

        try
        {
            await groupDeletionRepository.AddAsync(new GroupDeletion(message.Id, userId));
            await unitOfWork.SaveChangesAsync();
        }
        catch (Exception)
        {
            return Result.Failure(GroupMessageErrors.RemoveError);
        }

        return Result.Success();
    }

    public async Task<Result<ReactionDto>> AddReactionAsync(Guid userId, Guid groupId, Guid messageId,
        ReactionType type)
    {
        var membership = await groupMembershipRepository.GetByGroupIdAndMemberIdAsync(groupId, userId,
            true, includeRole: true);

        if (membership is null)
            return Result<ReactionDto>.Failure(GroupMembershipErrors.NotFound);

        if (!membership.Group.AllowReactions)
            return Result<ReactionDto>.Failure(GroupRoleErrors.NotAllowed);

        var message = await groupMessageRepository.GetByIdAsync(messageId, true);

        if (message is null
            || message.Deletions.Any(deletion => deletion.UserId == userId))
            return Result<ReactionDto>.Failure(GroupMessageErrors.NotFound);

        var reaction = await groupReactionRepository.GetByMessageIdAndUserIdAndTypeAsync(message.Id, userId, type);

        if (reaction is not null) return Result<ReactionDto>.Failure(GroupReactionErrors.AlreadyExist);

        reaction = new GroupReaction(message.Id, userId, type);

        try
        {
            await groupReactionRepository.AddAsync(reaction);
            await unitOfWork.SaveChangesAsync();
        }
        catch (Exception)
        {
            return Result<ReactionDto>.Failure(GroupReactionErrors.CreateError);
        }

        return Result<ReactionDto>.Success(mapper.Map<ReactionDto>(reaction));
    }

    public async Task<Result> RemoveReactionAsync(Guid userId, Guid groupId, Guid messageId, Guid reactionId)
    {
        var membership = await groupMembershipRepository.GetByGroupIdAndMemberIdAsync(groupId, userId,
            true, includeRole: true);

        if (membership is null)
            return Result<ReactionDto>.Failure(GroupMembershipErrors.NotFound);

        var message = await groupMessageRepository.GetByIdAsync(messageId, true);

        if (message is null
            || message.Deletions.Any(deletion => deletion.UserId == userId))
            return Result<ReactionDto>.Failure(GroupMessageErrors.NotFound);

        var reaction = await groupReactionRepository.GetByIdAsync(reactionId);

        if (reaction is null
            || reaction.UserId != userId
            || reaction.MessageId != message.Id)
            return Result.Failure(GroupReactionErrors.NotFound);

        try
        {
            groupReactionRepository.Remove(reaction);
            await unitOfWork.SaveChangesAsync();
        }
        catch (Exception)
        {
            return Result.Failure(GroupReactionErrors.RemoveError);
        }

        return Result.Success();
    }

    public async Task<Result<PagedList<BanDto>>> GetBannedMemberAsync(Guid userId, Guid groupId,
        MemberParameters parameters)
    {
        var members = await groupBanRepository.GetByGroupIdAsync(groupId, parameters);

        return Result<PagedList<BanDto>>.Success(
            members.Select(mapper.Map<BanDto>).ToPagedList(members));
    }

    public async Task<Result> BanMemberAsync(Guid userId, Guid groupId, Guid memberToBanId)
    {
        var membership = await groupMembershipRepository.GetByGroupIdAndMemberIdAsync(groupId, userId,
            includeRole: true);

        if (membership is null)
            return Result<ReactionDto>.Failure(GroupMembershipErrors.NotFound);

        if (membership.Role is null || !membership.Role.AllowBanMembers)
            return Result.Failure(GroupRoleErrors.NotAllowed);

        var userToBan = await userRepository.GetByIdAsync(userId);

        if (userToBan is null)
            return Result.Failure(UserErrors.NotFound);

        var userToBanMembership = await groupMembershipRepository.GetByGroupIdAndMemberIdAsync(groupId, userToBan.Id);

        if (userToBanMembership is null)
            return Result.Failure(GroupMembershipErrors.NotFound);

        var groupBan = new GroupBan(groupId, userToBan.Id);

        var transaction = await unitOfWork.BeginTransactionAsync();

        try
        {
            await groupBanRepository.AddAsync(groupBan);
            groupMembershipRepository.Remove(userToBanMembership);
            await unitOfWork.SaveChangesAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();

            return Result.Failure(GroupBanErrors.CreateError);
        }

        await transaction.CommitAsync();

        return Result.Success();
    }

    public async Task<Result> UnbanMemberAsync(Guid userId, Guid groupId, Guid memberToUnbanId)
    {
        var membership = await groupMembershipRepository.GetByGroupIdAndMemberIdAsync(groupId, userId,
            includeRole: true);

        if (membership is null)
            return Result<ReactionDto>.Failure(GroupMembershipErrors.NotFound);

        if (membership.Role is null || !membership.Role.AllowBanMembers)
            return Result.Failure(GroupRoleErrors.NotAllowed);

        var groupBan = await groupBanRepository.GetByGroupIdAndMemberIdAsync(groupId, memberToUnbanId);

        if (groupBan is null)
            return Result.Failure(GroupBanErrors.NotFound);

        try
        {
            groupBanRepository.Remove(groupBan);
            await unitOfWork.SaveChangesAsync();
        }
        catch (Exception)
        {
            return Result.Failure(GroupBanErrors.RemoveError);
        }

        return Result.Success();
    }

    public async Task<Result<PagedList<GroupInvitationDto>>> GetInvitationsAsync(Guid userId, Guid groupId,
        InvitationParameters parameters)
    {
        var invitations = await groupInvitationRepository.GetPagedByGroupIdAndUserIdAsync(groupId,
            userId, parameters);

        return Result<PagedList<GroupInvitationDto>>.Success(
            invitations.Select(mapper.Map<GroupInvitationDto>).ToPagedList(invitations));
    }

    public async Task<Result<GroupInvitationDto>> CreateInvitationLinkAsync(Guid userId, Guid groupId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> RemoveInvitationLinkAsync(Guid userId, Guid groupId, Guid invitationId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<PagedList<GroupMemberDto>>> GetMembersAsync(Guid userId, Guid groupId,
        MemberParameters parameters)
    {
        var members = await groupMembershipRepository.GetPagedByGroupIdAsync(groupId, parameters);

        return Result<PagedList<GroupMemberDto>>.Success(
            members.Select(mapper.Map<GroupMemberDto>).ToPagedList(members));
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