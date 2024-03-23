using AutoMapper;
using ChatApp.Server.Application.Contacts.Dtos;
using ChatApp.Server.Application.Core;
using ChatApp.Server.Application.Core.Abstractions;
using ChatApp.Server.Application.Shared.Dtos;
using ChatApp.Server.Domain.Contacts;
using ChatApp.Server.Domain.Contacts.Errors;
using ChatApp.Server.Domain.Contacts.Repositories;
using ChatApp.Server.Domain.Core.Abstractions.Results;
using ChatApp.Server.Domain.Resources;
using ChatApp.Server.Domain.Resources.Repositories;
using ChatApp.Server.Domain.Users.Errors;
using ChatApp.Server.Domain.Users.Repositories;

namespace ChatApp.Server.Application.Contacts;

public sealed class ContactService(
    IUserRepository userRepository,
    IContactRepository contactRepository,
    IUserAvatarRepository userAvatarRepository,
    IContactAvatarRepository contactAvatarRepository,
    IResourceRepository resourceRepository,
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
            var latestUserAvatar = await userAvatarRepository.GetLatestByUserIdAsync(contact.PartnerId);

            if (latestUserAvatar is null) return Result<ContactDto>.Success(dto);

            dto.Avatar = mapper.Map<PriorityAvatarDto>(latestUserAvatar);
        }

        return Result<ContactDto>.Success(dto);
    }

    public async Task<Result<Guid>> AddContactAsync(Guid userId, Guid partnerId, ContactNameDto dto)
    {
        var partner = await userRepository.GetByIdAsync(partnerId);

        if (partner is null)
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
        var contact = await contactRepository.GetByIdAsync(contactId);

        if (contact is null || contact.OwnerId != userId)
            return Result.Failure(ContactErrors.NotFound);

        try
        {
            contactRepository.Remove(contact);
            await unitOfWork.SaveChangesAsync();
        }
        catch (Exception)
        {
            return Result.Failure(ContactErrors.RemoveError);
        }

        return Result.Success();
    }

    public async Task<Result<ContactNameDto>> UpdateNameAsync(Guid userId, Guid contactId, ContactNameDto dto)
    {
        var contact = await contactRepository.GetByIdAsync(contactId);

        if (contact is null || contact.OwnerId != userId)
            return Result<ContactNameDto>.Failure(ContactErrors.NotFound);

        mapper.Map(dto, contact);

        try
        {
            contactRepository.Update(contact);
            await unitOfWork.SaveChangesAsync();
        }
        catch (Exception)
        {
            return Result<ContactNameDto>.Failure(ContactErrors.UpdateError);
        }

        return Result<ContactNameDto>.Success(mapper.Map<ContactNameDto>(contact));
    }

    public async Task<Result<PriorityAvatarDto>> SetAvatarAsync(Guid userId, Guid contactId, NewResourceDto dto)
    {
        if (!ImageValidator.IsValid(dto.Extension))
            return Result<PriorityAvatarDto>.Failure(ContactAvatarErrors.Invalid);

        var contact = await contactRepository.GetByIdAsync(contactId, includeAvatarResource: true);

        if (contact is null || contact.OwnerId != userId)
            return Result<PriorityAvatarDto>.Failure(ContactErrors.NotFound);

        var resource = contact.Avatar is not null
            ? contact.Avatar.Resource
            : new Resource();

        mapper.Map(dto, resource);

        var transaction = await unitOfWork.BeginTransactionAsync();

        try
        {
            if (contact.Avatar is not null)
            {
                resourceRepository.Update(resource);
            }
            else
            {
                await resourceRepository.AddAsync(resource);
                await contactAvatarRepository.AddAsync(new ContactAvatar(contact.Id, resource.Id));
            }

            await unitOfWork.SaveChangesAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();

            return Result<PriorityAvatarDto>.Failure(contact.Avatar is not null
                ? ContactAvatarErrors.UpdateError
                : ContactAvatarErrors.AddError);
        }

        await transaction.CommitAsync();

        var response = mapper.Map<PriorityAvatarDto>(contact.Avatar);

        return Result<PriorityAvatarDto>.Success(response);
    }

    public async Task<Result> RemoveAvatarAsync(Guid userId, Guid contactId)
    {
        var contact = await contactRepository.GetByIdAsync(contactId, includeAvatarResource: true);

        if (contact is null || contact.OwnerId != userId)
            return Result<PriorityAvatarDto>.Failure(ContactErrors.NotFound);

        if (contact.Avatar is null)
            return Result<PriorityAvatarDto>.Failure(ContactAvatarErrors.NotFound);

        try
        {
            resourceRepository.Remove(contact.Avatar.Resource);
            await unitOfWork.SaveChangesAsync();
        }
        catch (Exception)
        {
            return Result.Failure(ContactAvatarErrors.RemoveError);
        }

        return Result.Success();
    }
}