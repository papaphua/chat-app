using AutoMapper;
using ChatApp.Server.Application.Core.Abstractions;
using ChatApp.Server.Application.Directs.Dtos;
using ChatApp.Server.Application.Shared.Dtos;
using ChatApp.Server.Domain.Contacts.Repositories;
using ChatApp.Server.Domain.Core;
using ChatApp.Server.Domain.Core.Abstractions.Results;
using ChatApp.Server.Domain.Directs;
using ChatApp.Server.Domain.Directs.Errors;
using ChatApp.Server.Domain.Directs.Repositories;
using ChatApp.Server.Domain.Resources;
using ChatApp.Server.Domain.Resources.Repositories;
using ChatApp.Server.Domain.Users.Errors;
using ChatApp.Server.Domain.Users.Repositories;

namespace ChatApp.Server.Application.Directs;

public sealed class DirectService(
    IDirectRepository directRepository,
    IDirectMembershipRepository directMembershipRepository,
    IResourceRepository resourceRepository,
    IDirectAttachmentRepository directAttachmentRepository,
    IDirectMessageRepository directMessageRepository,
    IDirectDeletionRepository directDeletionRepository,
    IDirectReactionRepository directReactionRepository,
    IContactRepository contactRepository,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IDirectService
{
    public async Task<Result<DirectDto>> GetDirectAsync(Guid userId, Guid directId)
    {
        var direct = await directRepository.GetByIdAsync(directId, true, true);

        var userMembership = direct?.Memberships.FirstOrDefault(membership => membership.MemberId == userId);

        if (direct is null
            || userMembership is null
            || userMembership.IsChatSelfDeleted)
            return Result<DirectDto>.Failure(DirectErrors.NotFound);

        var partner = direct.Memberships.Where(membership => membership.MemberId != userId)
            .Select(membership => membership.Member)
            .First();

        var contact = await contactRepository.GetByOwnerIdAndPartnerId(userId, partner.Id, true);

        var dto = mapper.Map<DirectDto>(direct);

        mapper.Map(partner, dto);

        if (contact is not null)
            mapper.Map(contact, dto);

        dto.Avatars = dto.Avatars.OrderByDescending(avatar => avatar.Priority)
            .ToList();

        return Result<DirectDto>.Success(dto);
    }

    public async Task<Result<Guid>> CreateDirectAsync(Guid userId, Guid partnerId)
    {
        var partner = await userRepository.GetByIdAsync(partnerId);

        if (partner is null)
            return Result<Guid>.Failure(UserErrors.NotFound);

        var direct = await directRepository.GetByMemberIds(userId, partnerId);

        DirectMembership userMembership;

        if (direct is not null)
        {
            userMembership = direct.Memberships.First(membership => membership.MemberId == userId);

            if (!userMembership.IsChatSelfDeleted) return Result<Guid>.Success(direct.Id);

            userMembership.IsChatSelfDeleted = false;

            try
            {
                directMembershipRepository.Update(userMembership);
                await unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                return Result<Guid>.Failure(DirectErrors.CreateError);
            }

            return Result<Guid>.Success(direct.Id);
        }

        direct = new Direct();
        userMembership = new DirectMembership(direct.Id, userId);
        var partnerMembership = new DirectMembership(direct.Id, partner.Id);

        var transaction = await unitOfWork.BeginTransactionAsync();

        try
        {
            await directRepository.AddAsync(direct);
            await directMembershipRepository.AddRangeAsync(userMembership, partnerMembership);
            await unitOfWork.SaveChangesAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();

            return Result<Guid>.Failure(DirectErrors.CreateError);
        }

        await transaction.CommitAsync();

        return Result<Guid>.Success(direct.Id);
    }

    public async Task<Result> RemoveDirectAsync(Guid userId, Guid directId)
    {
        var direct = await directRepository.GetByIdAsync(directId, true);

        var userMembership = direct?.Memberships.FirstOrDefault(membership => membership.MemberId == userId);

        if (direct is null
            || userMembership is null
            || userMembership.IsChatSelfDeleted)
            return Result.Failure(DirectErrors.NotFound);

        try
        {
            directRepository.Remove(direct);
            await unitOfWork.SaveChangesAsync();
        }
        catch (Exception)
        {
            return Result.Failure(DirectErrors.RemoveError);
        }

        return Result.Success();
    }

    public async Task<Result> RemoveDirectForSelfAsync(Guid userId, Guid directId)
    {
        var direct = await directRepository.GetByIdAsync(directId, true);

        var userMembership = direct?.Memberships.FirstOrDefault(membership => membership.MemberId == userId);

        if (direct is null
            || userMembership is null
            || userMembership.IsChatSelfDeleted)
            return Result.Failure(DirectErrors.NotFound);

        var partnerMembership = direct.Memberships.First(membership => membership.MemberId != userId);

        if (partnerMembership.IsChatSelfDeleted)
        {
            try
            {
                directRepository.Remove(direct);
                await unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                return Result.Failure(DirectErrors.RemoveError);
            }

            return Result.Success();
        }

        userMembership.IsChatSelfDeleted = true;
        userMembership.ChatSelfDeletedTimestamp = DateTime.UtcNow;

        try
        {
            directMembershipRepository.Update(userMembership);
            await unitOfWork.SaveChangesAsync();
        }
        catch (Exception)
        {
            return Result.Failure(DirectErrors.RemoveError);
        }

        return Result.Success();
    }

    public async Task<Result<MessageDto>> AddMessageAsync(Guid userId, Guid directId, NewMessageDto dto)
    {
        var direct = await directRepository.GetByIdAsync(directId, true);

        var userMembership = direct?.Memberships.FirstOrDefault(membership => membership.MemberId == userId);

        if (direct is null
            || userMembership is null
            || userMembership.IsChatSelfDeleted)
            return Result<MessageDto>.Failure(DirectErrors.NotFound);

        var message = new DirectMessage(direct.Id, userId) { Content = dto.Content };

        var hasAttachments = dto.Attachments.Count > 0;
        List<Resource> resources = [];
        List<DirectAttachment> attachments = [];

        if (hasAttachments)
        {
            resources = dto.Attachments.Select(mapper.Map<Resource>).ToList();
            attachments = resources.Select(resource => new DirectAttachment(message.Id, resource.Id)).ToList();
        }

        var partnerMembership = direct.Memberships.First(membership => membership.MemberId != userId);

        var transaction = await unitOfWork.BeginTransactionAsync();

        try
        {
            await directMessageRepository.AddAsync(message);

            if (hasAttachments)
            {
                await resourceRepository.AddRangeAsync(resources);
                await directAttachmentRepository.AddRangeAsync(attachments);
            }

            if (partnerMembership.IsChatSelfDeleted)
            {
                partnerMembership.IsChatSelfDeleted = false;
                directMembershipRepository.Update(partnerMembership);
            }

            await unitOfWork.SaveChangesAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();

            return Result<MessageDto>.Failure(DirectMessageErrors.CreateError);
        }

        await transaction.CommitAsync();

        return Result<MessageDto>.Success(mapper.Map<MessageDto>(message));
    }

    public async Task<Result> RemoveMessageAsync(Guid userId, Guid directId, Guid messageId)
    {
        var direct = await directRepository.GetByIdAsync(directId, true);

        var userMembership = direct?.Memberships.FirstOrDefault(membership => membership.MemberId == userId);

        if (direct is null
            || userMembership is null
            || userMembership.IsChatSelfDeleted)
            return Result.Failure(DirectErrors.NotFound);

        var message = await directMessageRepository.GetByIdAsync(messageId);

        var attachments = await directAttachmentRepository.GetByMessageIdAsync(messageId, true);
        var resources = attachments.Select(attachment => attachment.Resource).ToList();

        if (message is null
            || message.DirectId != direct.Id)
            return Result.Failure(DirectMessageErrors.NotFound);

        var transaction = await unitOfWork.BeginTransactionAsync();

        try
        {
            if (resources.Count > 0)
                resourceRepository.RemoveRange(resources);

            directMessageRepository.Remove(message);
            
            await unitOfWork.SaveChangesAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();

            return Result.Failure(DirectMessageErrors.RemoveError);
        }

        await transaction.CommitAsync();

        return Result.Success();
    }

    public async Task<Result> RemoveMessageForSelfAsync(Guid userId, Guid directId, Guid messageId)
    {
        var direct = await directRepository.GetByIdAsync(directId, true);

        var userMembership = direct?.Memberships.FirstOrDefault(membership => membership.MemberId == userId);

        if (direct is null
            || userMembership is null
            || userMembership.IsChatSelfDeleted)
            return Result.Failure(DirectErrors.NotFound);

        var message = await directMessageRepository.GetByIdAsync(messageId, true);

        if (message is null
            || message.DirectId != direct.Id
            || message.Deletions.Any(deletion => deletion.UserId == userId))
            return Result.Failure(DirectMessageErrors.NotFound);

        var transaction = await unitOfWork.BeginTransactionAsync();

        try
        {
            if (message.Deletions.Count >= 1)
            {
                var attachments = await directAttachmentRepository.GetByMessageIdAsync(message.Id, true);
                var resources = attachments.Select(attachment => attachment.Resource);

                resourceRepository.RemoveRange(resources);
                directMessageRepository.Remove(message);
            }
            else
            {
                await directDeletionRepository.AddAsync(new DirectDeletion(message.Id, userId));
            }

            await unitOfWork.SaveChangesAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();

            return Result.Failure(DirectMessageErrors.RemoveError);
        }

        await transaction.CommitAsync();

        return Result.Success();
    }

    public async Task<Result<ReactionDto>> AddReactionAsync(Guid userId, Guid directId, Guid messageId,
        ReactionType type)
    {
        var direct = await directRepository.GetByIdAsync(directId, true);

        var userMembership = direct?.Memberships.FirstOrDefault(membership => membership.MemberId == userId);

        if (direct is null
            || userMembership is null
            || userMembership.IsChatSelfDeleted)
            return Result<ReactionDto>.Failure(DirectErrors.NotFound);

        var message = await directMessageRepository.GetByIdAsync(messageId, true);

        if (message is null
            || message.Deletions.Any(deletion => deletion.UserId == userId))
            return Result<ReactionDto>.Failure(DirectMessageErrors.NotFound);

        var reaction = await directReactionRepository.GetByMessageIdAndUserIdAndTypeAsync(message.Id, userId, type);

        if (reaction is not null) return Result<ReactionDto>.Failure(DirectReactionErrors.AlreadyExist);

        reaction = new DirectReaction(message.Id, userId, type);

        try
        {
            await directReactionRepository.AddAsync(reaction);
            await unitOfWork.SaveChangesAsync();
        }
        catch (Exception)
        {
            return Result<ReactionDto>.Failure(DirectReactionErrors.CreateError);
        }

        return Result<ReactionDto>.Success(mapper.Map<ReactionDto>(reaction));
    }

    public async Task<Result> RemoveReactionAsync(Guid userId, Guid directId, Guid messageId, Guid reactionId)
    {
        var direct = await directRepository.GetByIdAsync(directId, true);

        var userMembership = direct?.Memberships.FirstOrDefault(membership => membership.MemberId == userId);

        if (direct is null
            || userMembership is null
            || userMembership.IsChatSelfDeleted)
            return Result.Failure(DirectErrors.NotFound);

        var message = await directMessageRepository.GetByIdAsync(messageId, true);

        if (message is null
            || message.Deletions.Any(deletion => deletion.UserId == userId))
            return Result.Failure(DirectMessageErrors.NotFound);

        var reaction = await directReactionRepository.GetByIdAsync(reactionId);

        if (reaction is null
            || reaction.UserId != userId
            || reaction.MessageId != message.Id)
            return Result.Failure(DirectReactionErrors.NotFound);

        try
        {
            directReactionRepository.Remove(reaction);
            await unitOfWork.SaveChangesAsync();
        }
        catch (Exception)
        {
            return Result.Failure(DirectReactionErrors.RemoveError);
        }

        return Result.Success();
    }
}