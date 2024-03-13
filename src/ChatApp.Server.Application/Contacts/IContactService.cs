using ChatApp.Server.Application.Contacts.Dtos;
using ChatApp.Server.Application.Shared.Dtos;
using ChatApp.Server.Domain.Core.Abstractions.Results;

namespace ChatApp.Server.Application.Contacts;

public interface IContactService
{
    Task<Result<ContactDto>> GetContactAsync(Guid userId, Guid contactId);

    Task<Result<Guid>> AddContactAsync(Guid userId, Guid partnerId, NameDto dto);

    Task<Result> RemoveContactAsync(Guid userId, Guid contactId);

    Task<Result<NameDto>> UpdateNameAsync(Guid userId, Guid contactId, NameDto dto);

    Task<Result<PriorityAvatarDto>> SetAvatarAsync(Guid userId, Guid contactId, NewResourceDto dto);

    Task<Result> RemoveAvatarAsync(Guid userId, Guid contactId);
}