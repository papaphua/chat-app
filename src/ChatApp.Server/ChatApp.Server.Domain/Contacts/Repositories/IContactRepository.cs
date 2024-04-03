using ChatApp.Server.Domain.Core.Abstractions;
using ChatApp.Server.Domain.Core.Abstractions.Paging;

namespace ChatApp.Server.Domain.Contacts.Repositories;

public interface IContactRepository : IRepository<Contact>
{
    Task<PagedList<Contact>> GetPagedByOwnerId(Guid ownerId, ContactParameters parameters, bool includeAvatar = false,
        bool includeAvatarResource = false);

    Task<Contact?> GetByIdAsync(Guid id, bool includeAvatar = false, bool includeAvatarResource = false);

    Task<Contact?> GetByOwnerIdAndPartnerId(Guid ownerId, Guid partnerId, bool includeAvatar = false,
        bool includeAvatarResource = false);
}