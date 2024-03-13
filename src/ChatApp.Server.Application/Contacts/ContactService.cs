using AutoMapper;
using ChatApp.Server.Application.Contacts.Dtos;
using ChatApp.Server.Application.Core;
using ChatApp.Server.Application.Core.Abstractions;
using ChatApp.Server.Application.Shared.Dtos;
using ChatApp.Server.Domain.Contacts;
using ChatApp.Server.Domain.Contacts.Errors;
using ChatApp.Server.Domain.Contacts.Repositories;
using ChatApp.Server.Domain.Core.Abstractions.Results;
using ChatApp.Server.Domain.Users.Errors;
using ChatApp.Server.Domain.Users.Repositories;

namespace ChatApp.Server.Application.Contacts;

public sealed class ContactService(
    IUserRepository userRepository,
    IContactRepository contactRepository,
    IUserAvatarRepository userAvatarRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork) 
    : IContactService
{
    public async Task<Result<ContactDto>> GetContactAsync(Guid userId, Guid contactId)
    {
        var contact = await contactRepository.GetByIdAsync(contactId, true);

        if (contact is null || contact.OwnerId != userId)
            return Result<ContactDto>.Failure(ContactErrors.NotFound);

        var dto = mapper.Map<ContactDto>(contact);
        
        if (contact.Avatar is null)
        {
            var latestUserAvatar = await userAvatarRepository.GetLatestByIdAsync(contact.PartnerId);

            if (latestUserAvatar is null) return Result<ContactDto>.Success(dto);
            
            dto.Avatar = mapper.Map<PriorityAvatarDto>(latestUserAvatar);
            dto.Avatar.Priority = AvatarPriority.User;
        }
        else
        {
            dto.Avatar!.Priority = AvatarPriority.Contact;
        }

        return Result<ContactDto>.Success(dto);
    }

    public async Task<Result<Guid>> AddContactAsync(Guid userId, Guid partnerId, NameDto dto)
    {
        var partner = await userRepository.GetByIdAsync(partnerId);
        
        if(partner is null) 
            return Result<Guid>.Failure(UserErrors.NotFound);
        
        var contact = await contactRepository.GetByOwnerIdAndPartnerId(userId, partner.Id);

        if (contact is not null)
            return Result<Guid>.Failure(ContactErrors.AlreadyExist);

        contact = new Contact(userId, partner.Id);
        mapper.Map(dto, contact);

        try
        {
            await contactRepository.AddAsync(contact);
            await unitOfWork.SaveChangesAsync();
        }
        catch (Exception)
        {
            return Result<Guid>.Failure(ContactErrors.AddError);
        }
        
        return Result<Guid>.Success(contact.Id);
    }

    public async Task<Result> RemoveContactAsync(Guid userId, Guid contactId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<NameDto>> UpdateNameAsync(Guid userId, Guid contactId, NameDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<PriorityAvatarDto>> SetAvatarAsync(Guid userId, Guid contactId, NewResourceDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> RemoveAvatarAsync(Guid userId, Guid contactId)
    {
        throw new NotImplementedException();
    }
}