using AutoMapper;
using ChatApp.Server.Application.Core.Abstractions;
using ChatApp.Server.Application.Directs.Dtos;
using ChatApp.Server.Domain.Contacts.Repositories;
using ChatApp.Server.Domain.Core.Abstractions.Results;
using ChatApp.Server.Domain.Directs;
using ChatApp.Server.Domain.Directs.Errors;
using ChatApp.Server.Domain.Directs.Repositories;
using ChatApp.Server.Domain.Users.Errors;
using ChatApp.Server.Domain.Users.Repositories;

namespace ChatApp.Server.Application.Directs;

public sealed class DirectService(
    IDirectRepository directRepository,
    IDirectMembershipRepository directMembershipRepository,
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
}