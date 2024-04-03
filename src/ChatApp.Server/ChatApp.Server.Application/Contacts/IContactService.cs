using ChatApp.Server.Application.Contacts.Dtos;
using ChatApp.Server.Application.Shared.Dtos;
using ChatApp.Server.Domain.Contacts;
using ChatApp.Server.Domain.Core.Abstractions.Paging;
using ChatApp.Server.Domain.Core.Abstractions.Results;
using Microsoft.AspNetCore.Http;

namespace ChatApp.Server.Application.Contacts;

public interface IContactService
{
    Task<Result<PagedList<ContactDto>>> GetAllContactsAsync(Guid userId, ContactParameters parameters);
    
    Task<Result<ContactDto>> GetContactAsync(Guid userId, Guid contactId);

    Task<Result<Guid>> AddContactAsync(Guid userId, Guid partnerId, ContactNameDto dto);

    Task<Result> RemoveContactAsync(Guid userId, Guid contactId);

    Task<Result> UpdateNameAsync(Guid userId, Guid contactId, ContactNameDto dto);

    Task<Result<PriorityAvatarDto>> AddAvatarAsync(Guid userId, Guid contactId, IFormFile file);

    Task<Result> RemoveAvatarAsync(Guid userId, Guid contactId);
}