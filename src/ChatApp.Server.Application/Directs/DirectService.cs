using AutoMapper;
using ChatApp.Server.Application.Core.Abstractions;
using ChatApp.Server.Application.Directs.Dtos;
using ChatApp.Server.Domain.Contacts.Repositories;
using ChatApp.Server.Domain.Core.Abstractions.Results;
using ChatApp.Server.Domain.Directs.Errors;
using ChatApp.Server.Domain.Directs.Repositories;

namespace ChatApp.Server.Application.Directs;

public sealed class DirectService(
    IDirectRepository directRepository,
    IContactRepository contactRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IDirectService
{
    public async Task<Result<DirectDto>> GetDirectAsync(Guid userId, Guid directId)
    {
        var direct = await directRepository.GetByIdAsync(directId, true);

        var userMembership = direct?.Memberships.FirstOrDefault(membership => membership.MemberId == userId);

        if (direct is null
            || userMembership is null
            || userMembership.IsChatSelfDeleted)
            return Result<DirectDto>.Failure(DirectErrors.NotFound);

        var partner = direct.Memberships.Where(membership => membership.MemberId != userId)
            .Select(membership => membership.Member)
            .First();

        var contact = await contactRepository.GetByOwnerIdAndPartnerId(userId, partner.Id);

        var dto = mapper.Map<DirectDto>(direct);

        mapper.Map(partner, dto);

        if (contact is not null)
            mapper.Map(contact, dto);

        return Result<DirectDto>.Success(dto);
    }

    public async Task<Result<Guid>> CreateDirectAsync(Guid userId, Guid partnerId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> RemoveDirectAsync(Guid userId, Guid directId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> RemoveDirectForSelfAsync(Guid userId, Guid directId)
    {
        throw new NotImplementedException();
    }
}