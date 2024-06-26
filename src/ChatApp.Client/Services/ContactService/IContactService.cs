﻿using ChatApp.Client.Core.Paging;
using ChatApp.Client.Dtos;
using Microsoft.AspNetCore.Components.Forms;

namespace ChatApp.Client.Services.ContactService;

public interface IContactService
{
    Task<PagedResponse<ContactDto>> GetAllContacts(ContactParameters parameters);

    Task<ContactDto> GetContactAsync(Guid contactId);

    Task AddContactAsync(Guid partnerId, ContactNameDto dto);

    Task RemoveContactAsync(Guid contactId);

    Task UpdateNameAsync(Guid contactId, ContactNameDto dto);

    Task AddAvatarAsync(Guid contactId, IBrowserFile file);

    Task RemoveAvatarAsync(Guid contactId);
}